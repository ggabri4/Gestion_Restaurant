using Microsoft.Build.Framework;

namespace Gestion_Restaurant.Models
{
    public class Produit
    {
        public int Id { get; set; }
        public string Description { get; set; }
        [Required]
        public Double Prix { get; set; }
        [Required]
        public Boolean Dispo { get; set; }
        public ICollection<Commande> Commandes { get; set; }
    }
}
