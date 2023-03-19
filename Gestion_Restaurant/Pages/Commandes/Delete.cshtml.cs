using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Gestion_Restaurant.Data;
using Gestion_Restaurant.Models;
using System.Collections;
using NuGet.Versioning;
using Microsoft.AspNetCore.Authorization;

namespace Gestion_Restaurant.Pages.Commandes
{
    [Authorize(Roles = "Admin, Barman, Serveur")]
    public class DeleteModel : PageModel
    {
        private readonly Gestion_Restaurant.Data.ApplicationDbContext _context;

        public DeleteModel(Gestion_Restaurant.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Commande Commande { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Commande == null)
            {
                return NotFound();
            }

            var commande = await _context.Commande
                .Include(c => c.CommandeServiPar)
                .Include(c => c.CommandePreparerPar)
                .Include(c => c.CommandeProduits)
                .Include(c => c.CommandeTables)
                .Include(c => c.FactureRattacher.PaiementCommande)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (commande == null)
            {
                return NotFound();
            }
            else 
            {
                Commande = commande;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Commande == null)
            {
                return NotFound();
            }
            var commande = await _context.Commande
                .Include(c => c.CommandePreparerPar)
                .Include(c => c.CommandeProduits)
                .Include(c => c.CommandeTables)
                .Include(c => c.CommandeServiPar)
                .Include(c => c.FactureRattacher.PaiementCommande)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (commande != null)
            {
                Commande = commande;

                foreach (Barman b in Commande.CommandePreparerPar)
                {
                    Commande.CommandePreparerPar.Remove(b);
                }
                foreach (Table t in Commande.CommandeTables)
                {
                    Commande.CommandeTables.Remove(t);
                }
                foreach (Serveur s in Commande.CommandeServiPar)
                {
                    Commande.CommandeServiPar.Remove(s);
                }
                foreach (Produit p in Commande.CommandeProduits)
                {
                    Commande.CommandeProduits.Remove(p);
                }
                if(Commande.FactureRattacher != null)
                {
                    foreach (Paiement p in Commande.FactureRattacher.PaiementCommande)
                    {
                        _context.Paiement.Remove(p);
                    }
                    _context.Facture.Remove(Commande.FactureRattacher);
                }
                _context.Commande.Remove(Commande);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
