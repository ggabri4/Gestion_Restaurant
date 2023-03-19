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
        [Display(Name = "Facture")]
        public Facture? FactureRattacher { get; set; }

        //lien de navigation
        [Display(Name = "Table(s)")]
        public ICollection<Table> CommandeTables { get; set; }
        [Display(Name = "Barmen")]
        public ICollection<Barman> CommandePreparerPar { get; set; }
        [Display(Name = "Serveur(s)")]
        public ICollection<Serveur> CommandeServiPar { get; set; }
        [Display(Name = "Produit(s)")]
        public ICollection<Produit> CommandeProduits { get; set; }

        [Display(Name = "Montant total")]
        public double Montant
        {
            get
            {
                double Somme = 0;
                foreach(var produit in CommandeProduits)
                {
                    Somme += produit.Prix;
                }
                return Somme;
            }
        }

        [Display(Name = "Informations")]
        public string CommandeInfos
        {
            get 
            {
                return "Commande n°" + Id + " ( Montant : " + Montant + "€ )";
            }
        }
    }
}
