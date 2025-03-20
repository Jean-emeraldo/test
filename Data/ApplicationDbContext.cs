using Microsoft.EntityFrameworkCore;
using CrudDotNet.Models;

namespace CrudDotNet.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Produit> Produits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity <Categorie>()
            .HasMany(p => p.Produits)
            .WithOne(e => e.Categorie)
            .HasForeignKey(e => e.id_cat);
        }
    }
}
