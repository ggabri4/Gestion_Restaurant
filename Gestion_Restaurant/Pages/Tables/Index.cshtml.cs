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

namespace Gestion_Restaurant.Pages.Tables
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly Gestion_Restaurant.Data.ApplicationDbContext _context;

        public IndexModel(Gestion_Restaurant.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Table> Table { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Table != null)
            {
                Table = await _context.Table
                .Include(t => t.CommandeRattache).ToListAsync();
            }
        }
    }
}