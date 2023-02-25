using Gestion_Restaurant.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Restaurant.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Commande> Commande { get; set; }
        public DbSet<Produit> Produit { get; set; }
        public DbSet<Table> Table { get; set; }
        public DbSet<Barman> Barman { get; set; }  
        public DbSet<Paiement> Paiement { get; set; }
        public DbSet<Facture> Facture { get; set; }
        public DbSet<Serveur> Serveur { get; set; }
    }
}