using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Gestion_Restaurant.Data;
using Gestion_Restaurant.Models;
using Microsoft.AspNetCore.Authorization;

namespace Gestion_Restaurant.Pages.Factures
{
    [Authorize(Roles = "Admin, Caissier")]
    public class DetailsModel : PageModel
    {
        private readonly Gestion_Restaurant.Data.ApplicationDbContext _context;

        public DetailsModel(Gestion_Restaurant.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Facture Facture { get; set; }
      public Facture FactureLight { get; set; }
        public List<int> tableIds { get; set; }
      public List<Produit> Produits { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Facture == null)
            {
                return NotFound();
            }
            var factureLight = await _context.Facture
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            var facture = await _context.Facture
                .Include(f => f.CommandeFacturer)
                .Include(f => f.PaiementCommande)
                .FirstOrDefaultAsync(m => m.Id == id);

            tableIds = _context.Facture
                .Include(f => f.CommandeFacturer)
                .Where(m => m.Id == id)
                .SelectMany(f => f.CommandeFacturer.CommandeTables.Select(ct => ct.Id))
                .Distinct()
                .ToList();

            Produits = _context.Facture
                .Include(f => f.CommandeFacturer.CommandeProduits)
                .Where(m => m.Id == id)
                .SelectMany(f => f.CommandeFacturer.CommandeProduits)
                .ToList();


            if (facture == null)
            {
                return NotFound();
            }
            else 
            {
                FactureLight = factureLight;
                Facture = facture;
            }
            return Page();
        }
    }
}
