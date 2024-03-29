﻿using MessagePack;
using System.ComponentModel.DataAnnotations;

namespace Gestion_Restaurant.Models
{
    public class Serveur
    {
        public int Id { get; set; }

        [Required]
        public string Nom { get; set; }
    
        [Required]
        public string Prenom { get; set; }

        // Clé étrangère vers la formation suivie
        [Display(Name = "Commande Rattachée")]
        public int? CommandeEtablitID { get; set; }
        public Commande? CommandeEtablit { get; set; }
    }
}
