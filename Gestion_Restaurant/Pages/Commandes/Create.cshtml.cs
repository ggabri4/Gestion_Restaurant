using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Gestion_Restaurant.Data;
using Gestion_Restaurant.Models;
using Microsoft.Extensions.Primitives;

namespace Gestion_Restaurant.Pages.Commandes
{
    public class CreateModel : PageModel
    {
        private readonly Gestion_Restaurant.Data.ApplicationDbContext _context;

        public CreateModel(Gestion_Restaurant.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["BarmanId"] = new SelectList(_context.Barman.Where(b => b.PrepareCommandeID == null).ToList(), "Id", "NomComplet");
            ViewData["ServeurId"] = new SelectList(_context.Serveur.Where(s => s.CommandeEtablitID == null).ToList(), "Id", "NomComplet");
            ViewData["TableId"] = new SelectList(_context.Table.Where(t => t.CommandeRattacheID == null).ToList(), "Id", "TableInfos");
            ViewData["ProduitId"] = new SelectList(_context.Produit.Where(p => p.Dispo == true).ToList(), "Id", "Description");

            return Page();
        }

        [BindProperty]
        public Commande Commande { get; set; }
        [BindProperty]
        public List<int> ServeursIds { get; set; }
        [BindProperty]
        public List<int> BarmenIds { get; set; }
        [BindProperty]
        public List<int> TablesIds { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Commande.DateTime = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return Page();
            }
            foreach (int ServeurId in ServeursIds)
            {
                Serveur? serveur = _context.Serveur.Find(ServeurId);
                if(serveur != null)
                {
                    Commande.CommandeServiPar.Add(serveur);
                }
            }
            foreach (int BarmanId in BarmenIds)
            {
                Barman? barman = _context.Barman.Find(BarmanId);
                if(barman != null)
                {
                    Commande.CommandePreparerPar.Add(barman);
                }
            }
            foreach (int TableId in TablesIds)
            {
                Table? table = _context.Table.Find(TableId);
                if(table != null) 
                { 
                    Commande.CommandeTables.Add(table);
                }
            }

            if(Request.Form.TryGetValue("produits", out StringValues ProduitsIds))
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
            

            _context.Commande.Add(Commande);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
