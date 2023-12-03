using Microsoft.EntityFrameworkCore;
using AspProject.Models;

namespace AspProject.Models
{
    public class ApplicationdbContext : DbContext
    {
        public ApplicationdbContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<Movie>? movies { get; set; }
        public DbSet<Genre> genres { get; set; }
        public DbSet<MemberShipType> memberShipTypes { get; set; }
        public DbSet<Customer>? customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Read the content of the JSON file
            string genreJson = System.IO.File.ReadAllText("Genres.json");
            // Deserialize the JSON into a list of Genre objects
            List<Genre> genres = System.Text.Json.JsonSerializer.Deserialize<List<Genre>>(genreJson);
            //Seed to categorie
            foreach (Genre c in genres)
                modelBuilder.Entity<Genre>()
                .HasData(c);
            /*
                        // Configuration de la relation Movie-Genre
                        modelBuilder.Entity<Movie>()
                            .HasOne(m => m.Genre)
                            .WithMany(g => g.Movies)
                            .HasForeignKey(m => m.GenreId);


                        // Configuration de la relation Customer-Membershiptype
                        modelBuilder.Entity<Customer>()
                            .HasOne(m => m.Membershiptype)
                            .WithMany(g => g.Customers)
                            .HasForeignKey(m => m.MembershiptypeId);*/
        }
    }
}
