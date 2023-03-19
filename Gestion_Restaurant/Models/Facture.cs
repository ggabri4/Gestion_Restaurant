using System.ComponentModel.DataAnnotations;

namespace Gestion_Restaurant.Models
{
    public class Facture
    {
        public Guid Id { get; set; }
        [Required]
        public Double Montant { get; set; }


        // Clé étrangère 
        [Display(Name = "Commande associée")]
        public int? CommandeFacturerID { get; set; }
        [Display(Name = "Commande associée")]
        public Commande? CommandeFacturer { get; set; }



        //lien de navigation
        public ICollection<Paiement>? PaiementCommande { get; set; }

        [Display(Name = "Informations")]
        public string FactureInfos
        {
            get
            {
                return "Facture n°" + Id + " ( " + Montant + "€ en " + PaiementCommande?.Count + " paiements)";
            }
        }
    }
}
