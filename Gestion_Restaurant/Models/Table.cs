﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Gestion_Restaurant.Models
{
    public class Table
    {
        public int Id { get; set; }
        public Boolean Etat { get; set; }

        [Display(Name = "Nombre de places")]
        public int NbPlace { get; set; }

        //Clé étrangère vers la commande rattaché
        [Display(Name = "Commande Rattachée")]
        public int? CommandeRattacheID { get; set; }
        [Display(Name = "Commande Rattachée")]
        public Commande? CommandeRattache { get; set; }

        [Display(Name = "Nom complet")]
        public string TableInfos
        {
            get
            {
                return "Table n°"+Id+" ( "+NbPlace+" Places )";
            }
        }
    }
}
