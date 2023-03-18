using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion_Restaurant.Models
{
    public class Commande
    {
        public int Id { get; set; }

        [Display(Name = "Passage de la commande")]
        public DateTime DateTime { get; set; }
        
        public string Statut { get; set; }

        //clé étrangère
        public Facture? FactureRattacher { get; set; }

        //lien de navigation
        public ICollection<Table> CommandeTables { get; set; }
        public ICollection<Barman> CommandePreparerPar { get; set; }
        public ICollection<Serveur> CommandeServiPar { get; set; }
        public ICollection<Produit> CommandeProduits { get; set; }
    }
}
