using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Gestion_Restaurant.Data;
using Gestion_Restaurant.Models;

namespace Gestion_Restaurant.Pages.Serveurs
{
    public class DetailsModel : PageModel
    {
        private readonly Gestion_Restaurant.Data.ApplicationDbContext _context;

        public DetailsModel(Gestion_Restaurant.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Serveur Serveur { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Serveur == null)
            {
                return NotFound();
            }

            var serveur = await _context.Serveur.FirstOrDefaultAsync(m => m.Id == id);
            if (serveur == null)
            {
                return NotFound();
            }
            else 
            {
                Serveur = serveur;
            }
            return Page();
        }
    }
}
