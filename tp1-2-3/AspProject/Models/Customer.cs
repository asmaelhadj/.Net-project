using System.ComponentModel.DataAnnotations.Schema;

namespace AspProject.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Movie>? Movies { get; set; }

        [ForeignKey("MembershiptypeId")]  // Define the foreign key relationship
        public Guid? MembershiptypeId { get; set; }

        public virtual MemberShipType? Membershiptype { get; set; }  // Navigation property to Membershiptype

    }
}
