using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Gestion_Restaurant.Data;
using Gestion_Restaurant.Models;
using Microsoft.AspNetCore.Authorization;

namespace Gestion_Restaurant.Pages.Tables
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly Gestion_Restaurant.Data.ApplicationDbContext _context;

        public CreateModel(Gestion_Restaurant.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CommandeRattacheID"] = new SelectList(_context.Commande, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Table Table { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Table.Add(Table);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
