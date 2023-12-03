using System.ComponentModel.DataAnnotations.Schema;

namespace AspProject.Models
{
    public class Movie
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public List<Customer>? Customers { get; set; }

        [ForeignKey("GenreId")]  // Define the foreign key relationship
        public Guid GenreId { get; set; }
        public virtual Genre? Genre { get; set; }  // Navigation property to Genre
        public string? Image { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public DateTime? CreatedDate { get; set; }

        public Movie()
        {

        }
    }
}
