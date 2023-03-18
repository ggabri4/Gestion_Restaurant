﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Gestion_Restaurant.Data;
using Gestion_Restaurant.Models;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace Gestion_Restaurant.Pages.Factures
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
            ViewData["CommandeFacturerID"] = new SelectList(_context.Commande, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Facture Facture { get; set; }

        [BindProperty]
        public Paiement Paiement { get; set; }

        [BindProperty(SupportsGet = true)]
        public List<Paiement> Paiements { get; set; }

        public string PaiementsJson { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            PaiementsJson = Request.Form["Facture_Paiements"];

            if (!ModelState.IsValid || PaiementsJson == null)
            {
                ViewData["CommandeFacturerID"] = new SelectList(_context.Commande, "Id", "Id");
                return Page();
            }
            Paiements = JsonConvert.DeserializeObject<List<Paiement>>(PaiementsJson);
            Facture.PaiementCommande = new List<Paiement>();

            if(Paiements != null)
            {
                foreach (var paiement in Paiements)
                {
                    paiement.FactureAPayer = Facture;
                    Facture.PaiementCommande.Add(paiement);
                }
            }
            
            Commande? c = _context.Commande.Find(Facture.CommandeFacturerID);
            if (c != null)
            {
                c.Statut = "Reglée";
                _context.Update(c);
            }
            try
            {
                _context.Facture.Add(Facture);
                await _context.SaveChangesAsync();
            }catch(Exception ex)
            {
                if(ex != null)//Pour le warning 
                    ModelState.AddModelError("Duplicate", "Il existe déjà une facture pour cette commande");
                ViewData["CommandeFacturerID"] = new SelectList(_context.Commande, "Id", "Id");
                return Page();
            }
            return RedirectToPage("./Index");
        } 
    }
}
