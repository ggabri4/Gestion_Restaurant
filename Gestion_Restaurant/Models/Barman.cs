
using System.ComponentModel.DataAnnotations;

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
        [Display(Name = "Commande Rattachée")]
        public int? PrepareCommandeID { get; set; }

        //lien de navigation
        public Commande? PrepareCommande { get; set; }

        [Display(Name = "Nom complet")]
        public string NomComplet
        {
            get
            {
                return Nom + " " + Prenom;
            }
        }
    }
}
