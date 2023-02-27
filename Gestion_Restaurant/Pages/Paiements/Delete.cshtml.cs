using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Gestion_Restaurant.Data;
using Gestion_Restaurant.Models;

namespace Gestion_Restaurant.Pages.Paiements
{
    public class DeleteModel : PageModel
    {
        private readonly Gestion_Restaurant.Data.ApplicationDbContext _context;

        public DeleteModel(Gestion_Restaurant.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Paiement Paiement { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Paiement == null)
            {
                return NotFound();
            }

            var paiement = await _context.Paiement.FirstOrDefaultAsync(m => m.Id == id);

            if (paiement == null)
            {
                return NotFound();
            }
            else 
            {
                Paiement = paiement;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Paiement == null)
            {
                return NotFound();
            }
            var paiement = await _context.Paiement.FindAsync(id);

            if (paiement != null)
            {
                Paiement = paiement;
                _context.Paiement.Remove(Paiement);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
