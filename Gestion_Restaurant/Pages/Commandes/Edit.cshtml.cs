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
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace Gestion_Restaurant.Pages.Commandes
{
    public class EditModel : PageModel
    {
        private readonly Gestion_Restaurant.Data.ApplicationDbContext _context;

        public EditModel(Gestion_Restaurant.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Commande Commande { get; set; } = default!;
        [BindProperty]
        public List<int>? ServeursIds { get; set; }
        [BindProperty]
        public List<int>? BarmenIds { get; set; }
        [BindProperty]
        public List<int>? TablesIds { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Commande == null)
            {
                return NotFound();
            }

            var commande =  await _context.Commande
                .Include(c => c.CommandeServiPar)
                .Include(c => c.CommandePreparerPar)
                .Include(c => c.CommandeProduits)
                .Include(c => c.CommandeTables)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (commande == null)
            {
                return NotFound();
            }
            var barmanIds = commande.CommandePreparerPar.Select(b => b.Id);
            var serveurId = commande.CommandeServiPar.Select(s => s.Id);
            var tableId = commande.CommandeTables.Select(t => t.Id);
            var produitId = commande.CommandeProduits.Select(p => p.Id);
            ViewData["BarmanId"] = new MultiSelectList(_context.Barman.Where(b => b.PrepareCommandeID == null || b.PrepareCommandeID == id).ToList(), "Id", "NomComplet", barmanIds);
            ViewData["ServeurId"] = new MultiSelectList(_context.Serveur.Where(s => s.CommandeEtablitID == null || s.CommandeEtablitID == id).ToList(), "Id", "NomComplet", serveurId);
            ViewData["TableId"] = new MultiSelectList(_context.Table.Where(t => t.CommandeRattacheID == null || t.CommandeRattacheID == id).ToList(), "Id", "TableInfos", tableId);
            ViewData["ProduitId"] = new MultiSelectList(_context.Produit.Where(p => p.Dispo == true).ToList(), "Id", "Description", produitId);

            Commande = commande;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var commande = await _context.Commande
                .Include(c => c.CommandePreparerPar)
                .Include(c => c.CommandeProduits)
                .Include(c => c.CommandeTables)
                .Include(c => c.CommandeServiPar)
                .Include(c => c.FactureRattacher.PaiementCommande)
                .FirstOrDefaultAsync(c => c.Id == id);

            commande.Statut = Commande.Statut;
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
            }
            foreach (int ServeurId in ServeursIds)
            {
                Serveur? serveur = _context.Serveur.Find(ServeurId);
                if (serveur != null)
                {
                    Commande.CommandeServiPar.Add(serveur);
                }
            }
            foreach (int BarmanId in BarmenIds)
            {
                Barman? barman = _context.Barman.Find(BarmanId);
                if (barman != null)
                {
                    Commande.CommandePreparerPar.Add(barman);
                }
            }
            foreach (int TableId in TablesIds)
            {
                Table? table = _context.Table.Find(TableId);
                if (table != null)
                {
                    Commande.CommandeTables.Add(table);
                }
            }

            if (Request.Form.TryGetValue("produits", out StringValues ProduitsIds))
            {
                foreach (string ProduitId in ProduitsIds)
                {
                    Produit? produit = _context.Produit.Find(int.Parse(ProduitId));
                    if (produit != null)
                    {
                        Commande.CommandeProduits.Add(produit);
                    }
                }
            }
            _context.Update(Commande);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommandeExists(Commande.Id))
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

        private bool CommandeExists(int id)
        {
          return _context.Commande.Any(e => e.Id == id);
        }
    }
}
