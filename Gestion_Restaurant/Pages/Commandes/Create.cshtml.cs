using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Gestion_Restaurant.Data;
using Gestion_Restaurant.Models;

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
            ViewData["BarmanId"] = new SelectList(_context.Barman, "Id", "NomComplet");
            ViewData["TableId"] = new SelectList(_context.Table, "Id", "TableInfos");

            return Page();
        }

        [BindProperty]
        public Commande Commande { get; set; }
        public Table Table { get; set; }
        public Barman Barman { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Commande.Add(Commande);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
