namespace Gestion_Restaurant.Models
{
    public class Table
    {
        public int Id { get; set; }
        public Boolean Etat { get; set; }

        //Clé étrangère vers la commande rattaché

        public int? CommandeRattacheID { get; set; }
        public Commande CommandeRattache { get; set; }
    }
}
