using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Gestion_Restaurant.Data;
using Gestion_Restaurant.Models;

namespace Gestion_Restaurant.Pages.Commandes
{
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

            var commande = await _context.Commande.FirstOrDefaultAsync(m => m.Id == id);

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
            var commande = await _context.Commande.FindAsync(id);

            if (commande != null)
            {
                Commande = commande;
                _context.Commande.Remove(Commande);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
