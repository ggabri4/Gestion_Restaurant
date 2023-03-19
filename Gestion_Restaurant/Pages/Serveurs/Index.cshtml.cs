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

namespace Gestion_Restaurant.Pages.Serveurs
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly Gestion_Restaurant.Data.ApplicationDbContext _context;

        public IndexModel(Gestion_Restaurant.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Serveur> Serveur { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Serveur != null)
            {
                Serveur = await _context.Serveur
                .Include(s => s.CommandeEtablit).ToListAsync();
            }
        }
    }
}
