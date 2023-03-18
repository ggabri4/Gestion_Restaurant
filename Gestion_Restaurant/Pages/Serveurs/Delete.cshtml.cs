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
    public class DeleteModel : PageModel
    {
        private readonly Gestion_Restaurant.Data.ApplicationDbContext _context;

        public DeleteModel(Gestion_Restaurant.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Serveur == null)
            {
                return NotFound();
            }
            var serveur = await _context.Serveur.FindAsync(id);

            if (serveur != null)
            {
                Serveur = serveur;
                _context.Serveur.Remove(Serveur);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
