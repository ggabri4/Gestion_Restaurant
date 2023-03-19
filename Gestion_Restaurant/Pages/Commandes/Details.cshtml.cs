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

namespace Gestion_Restaurant.Pages.Commandes
{
    [Authorize(Roles = "Admin, Barman, Serveur")]
    public class DetailsModel : PageModel
    {
        private readonly Gestion_Restaurant.Data.ApplicationDbContext _context;

        public DetailsModel(Gestion_Restaurant.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
