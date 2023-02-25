using Microsoft.Build.Framework;
using Microsoft.VisualBasic;

namespace Gestion_Restaurant.Models
{
    public class Facture
    {
        public Guid Id { get; set; }
        [Required]
        public Double Montant { get; set; }

        // Clé étrangère 
        //public int? CommandeFacturerID { get; set; }
        //public Commande CommandeFacturer { get; set; }


        //lien de navigation
        public ICollection<Commande> FacturationCommande { get; set; }
        public ICollection<Paiement> PaiementCommande { get; set; }
    }
}
