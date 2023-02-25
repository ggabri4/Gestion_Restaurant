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
    public class IndexModel : PageModel
    {
        private readonly Gestion_Restaurant.Data.ApplicationDbContext _context;

        public IndexModel(Gestion_Restaurant.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Commande> Commande { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public async Task OnGetAsync()
        {
            //Commande = await _context.Commande.ToListAsync();
            var e = from commande in _context.Commande 
                    select commande;
            if (!string.IsNullOrEmpty(SearchString))
            {
                e = e.Where(s => s.Statut.Contains(SearchString));
            }
            Commande = await e.ToListAsync();
        }
    }
}
