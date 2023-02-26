using Microsoft.Build.Framework;

namespace Gestion_Restaurant.Models
{
    public class Paiement
    {
        public int Id { get; set; }

        [Required]
        public string MoyenPaiement { get; set; }

        public double Montant { get; set; }

        //clé étrangère
        public Facture FactureAPayer { get; set; }
    }
}
