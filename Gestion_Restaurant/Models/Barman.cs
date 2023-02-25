using Microsoft.Build.Framework;

namespace Gestion_Restaurant.Models
{
    public class Barman
    {
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }

        // Clé étrangère 
        public int? CommandeEnChargeID { get; set; }

        //lien de navigation
        public Commande PrepareCommande { get; set; }
    }
}
