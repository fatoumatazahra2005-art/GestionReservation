using Microsoft.EntityFrameworkCore;
using webapisecond.Models;

namespace webapisecond.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Salle> Salles => Set<Salle>();

        public DbSet<Enseignant> Enseignants => Set<Enseignant>();

        public DbSet<Utilisateur> Utilisateurs => Set<Utilisateur>();

        public DbSet<Reservation> Reservations => Set<Reservation>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<Salle>().HasData(
                new Salle
                {
                    Id = 1,
                    Nom = "Amphi A",
                    Capacite = 150,
                    Batiment = "Bâtiment Principal"
                },

                new Salle
                {
                    Id = 2,
                    Nom = "Salle TP 12",
                    Capacite = 30,
                    Batiment = "Bâtiment Info"
                }
            );

           

            modelBuilder.Entity<Enseignant>().HasData(
                new Enseignant
                {
                    Id = 1,
                    Nom = "Ndiaye",
                    Prenom = "Omar",
                    Email = "o.ndiaye@univ.sn",
                    Departement = "Informatique"
                }
            );

            
            //MotDePasse123!

            modelBuilder.Entity<Utilisateur>().HasData(
            new Utilisateur
        {
            Id = 1,
            Email = "o.ndiaye@univ.sn",
            MotDePasseHash = "$2b$10$i/NwxNXfMM8ukMk/8qBIYeQol8qNf1kTUTfZDRgaP1f9MtCjbESjS",
            Role = "Enseignant"
        }
    );
        }
    }
}

