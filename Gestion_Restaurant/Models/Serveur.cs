using MessagePack;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace Gestion_Restaurant.Models
{
    public class Serveur
    {
        public int Id { get; set; }

        
        public string Nom { get; set; }
    
        public string Prenom { get; set; }

        // Clé étrangère vers la formation suivie
        public int? CommandeEtablitID { get; set; }
        public Commande? CommandeEtablit { get; set; }
    }
}
