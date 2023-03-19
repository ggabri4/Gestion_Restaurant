using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gestion_Restaurant.Data;
using Gestion_Restaurant.Models;
using Newtonsoft.Json;
using System.Text.Json;
using System.ComponentModel.Design;
using Microsoft.AspNetCore.Authorization;

namespace Gestion_Restaurant.Pages.Factures
{
    [Authorize(Roles = "Admin, Caissier")]
    public class EditModel : PageModel
    {
        private readonly Gestion_Restaurant.Data.ApplicationDbContext _context;

        public EditModel(Gestion_Restaurant.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Facture Facture { get; set; } = default!;

        [BindProperty]
        public Paiement Paiement { get; set; }

        [BindProperty(SupportsGet = true)]
        public List<Paiement> Paiements { get; set; }

        [BindProperty]
        public string PaiementsJson { get; set; }
        public string MontantsJson { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Facture == null)
            {
                return NotFound();
            }

            var facture =  await _context.Facture
                .Include(f => f.PaiementCommande)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (facture == null)
            {
                return NotFound();
            }
            Facture = facture;
            ViewData["CommandeFacturerID"] = new SelectList(_context.Commande.Include(c => c.CommandeProduits), "Id", "CommandeInfos");
            Facture.Montant = _context.Commande.Include(c => c.CommandeProduits).First().Montant;
            MontantsJson = System.Text.Json.JsonSerializer.Serialize(_context.Commande
                .Include(c => c.CommandeProduits)
                .Select(c => new Commande
                {
                    Id = c.Id,
                    CommandeProduits = c.CommandeProduits.Select(p => new Produit { Prix = p.Prix }).ToList()
                })
                .ToList());

            PaiementsJson = System.Text.Json.JsonSerializer.Serialize(facture.PaiementCommande
                .Select(p => new Paiement { Id = p.Id, Montant = p.Montant, MoyenPaiement = p.MoyenPaiement }));
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            PaiementsJson = Request.Form["Facture_Paiements"];
            MontantsJson = System.Text.Json.JsonSerializer.Serialize(_context.Commande
                .Include(c => c.CommandeProduits)
                .Select(c => new Commande
                {
                    Id = c.Id,
                    CommandeProduits = c.CommandeProduits.Select(p => new Produit { Prix = p.Prix }).ToList()
                })
                .ToList());
            //On recharche le json pour le bon fonctionnement si retour à page()

            if (ModelState.ErrorCount > 1 || PaiementsJson == null)//PaiementJson ne passe pas dans ModelState
            {

                ViewData["CommandeFacturerID"] = new SelectList(_context.Commande, "Id", "Id");
                return Page();
            }
            var listPaiements = await _context.Paiement
                .Where(p => p.FactureAPayer == Facture)
                .ToListAsync();

            Paiements = JsonConvert.DeserializeObject<List<Paiement>>(PaiementsJson);
            Facture.PaiementCommande = new List<Paiement>();

            foreach (Paiement p in listPaiements)
            {
                _context.Paiement.Remove(p);
            }//On retire les paiements dans un premier temps

            if (Paiements != null)
            {
                foreach (var paiement in Paiements)
                {
                    paiement.FactureAPayer = Facture;
                    Facture.PaiementCommande.Add(paiement);
                }
            }
            Commande? c = _context.Commande.Find(Facture.CommandeFacturerID);//Change le statut de la nouvelle commande séléctionnée
            if (c != null)
            {
                c.Statut = "Reglée";
                _context.Update(c);
            }

            _context.Attach(Facture).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FactureExists(Facture.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool FactureExists(Guid id)
        {
          return _context.Facture.Any(e => e.Id == id);
        }
    }
}
