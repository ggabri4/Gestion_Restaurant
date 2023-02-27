using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Gestion_Restaurant.Data;
using Gestion_Restaurant.Models;

namespace Gestion_Restaurant.Pages.Barmen
{
    public class DeleteModel : PageModel
    {
        private readonly Gestion_Restaurant.Data.ApplicationDbContext _context;

        public DeleteModel(Gestion_Restaurant.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Barman Barman { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Barman == null)
            {
                return NotFound();
            }

            var barman = await _context.Barman.FirstOrDefaultAsync(m => m.Id == id);

            if (barman == null)
            {
                return NotFound();
            }
            else 
            {
                Barman = barman;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Barman == null)
            {
                return NotFound();
            }
            var barman = await _context.Barman.FindAsync(id);

            if (barman != null)
            {
                Barman = barman;
                _context.Barman.Remove(Barman);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
