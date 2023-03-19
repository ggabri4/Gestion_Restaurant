using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Gestion_Restaurant.Models
{
    public class Paiement
    {
        public int Id { get; set; }

        [Display(Name = "Moyen de paiement")]
        public string MoyenPaiement { get; set; }

        public double? Montant { get; set; }

        //clé étrangère
        [Display(Name = "Facture")]
        public Facture? FactureAPayer { get; set; }

        [Display(Name = "Informations")]
        public string PaiementInfos
        {
            get
            {
                return "Paiement n°" + Id + " ( " + Montant + "€ par " + MoyenPaiement + " )";
            }
        }
    }
}
