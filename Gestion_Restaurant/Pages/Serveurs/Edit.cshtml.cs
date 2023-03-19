using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gestion_Restaurant.Data;
using Gestion_Restaurant.Models;
using Microsoft.AspNetCore.Authorization;

namespace Gestion_Restaurant.Pages.Serveurs
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly Gestion_Restaurant.Data.ApplicationDbContext _context;

        public EditModel(Gestion_Restaurant.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Serveur Serveur { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Serveur == null)
            {
                return NotFound();
            }

            var serveur =  await _context.Serveur.FirstOrDefaultAsync(m => m.Id == id);
            if (serveur == null)
            {
                return NotFound();
            }
            Serveur = serveur;
           ViewData["CommandeEtablitID"] = new SelectList(_context.Commande, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Serveur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServeurExists(Serveur.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ServeurExists(int id)
        {
          return _context.Serveur.Any(e => e.Id == id);
        }
    }
}
