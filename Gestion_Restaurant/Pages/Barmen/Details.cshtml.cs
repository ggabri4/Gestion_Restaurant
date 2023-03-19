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

namespace Gestion_Restaurant.Pages.Barmen
{
    [Authorize(Roles = "Admin")]
    public class DetailsModel : PageModel
    {
        private readonly Gestion_Restaurant.Data.ApplicationDbContext _context;

        public DetailsModel(Gestion_Restaurant.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Barman Barman { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Barman == null)
            {
                return NotFound();
            }

            var barman = await _context.Barman
                .Include(b => b.PrepareCommande)
                .FirstOrDefaultAsync(m => m.Id == id);
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
    }
}
