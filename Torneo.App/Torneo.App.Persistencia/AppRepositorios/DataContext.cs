using Microsoft.EntityFrameworkCore;
using Torneo.App.Dominio;
using System;
namespace Torneo.App.Persistencia
{
    public class DataContext : DbContext
    {

        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<DirectorTecnico> DirectoresTecnicos { get; set; }
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<Partido> Partidos { get; set; }
        public DbSet<Posicion> Posiciones { get; set; }
        public DbSet<Jugador> Jugadores { get; set; }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //DB en local    
                //optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Torneo");

                //DB azure
                //optionsBuilder.UseSqlServer("Server=tcp:torneo-futbol.database.windows.net,1433;Initial Catalog=Torneo;Persist Security Info=False;User ID=admin1;Password='Torneo;App.';MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

                // Read the SA_PASSWORD environment variable
                //string saPassword = Environment.GetEnvironmentVariable("MSSQL_SA_PASSWORD") ?? throw new InvalidOperationException("Environment variable MSSQL_SA_PASSWORD is not set.");

                // Use the SA_PASSWORD in the connection string
                optionsBuilder.UseSqlServer($"Server=db;Database=Torneo;User Id=sa;Password=${{ secrets.MSSQL_SA_PASSWORD }};MultipleActiveResultSets=true;Encrypt=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var relationship in
            modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

           /* modelBuilder.Entity<Municipio>()
                .HasIndex(x => x.Nombre)
                .IsUnique()*/
                
        }








    }
}