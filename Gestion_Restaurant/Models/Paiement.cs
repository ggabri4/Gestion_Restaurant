using Microsoft.Build.Framework;

namespace Gestion_Restaurant.Models
{
    public class Paiement
    {
        public int Id { get; set; }

        [Required]
        public string MoyenPaiement { get; set; }

        //clé étrangère
        public int? FactureAPayerID { get; set; }
        public Facture FactureAPayer { get; set; }
    }
}
