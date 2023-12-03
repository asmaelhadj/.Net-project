namespace AspProject.Models
{
    public class MemberShipType
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public double SignUpFee { get; set; }

        public int DurationInMonth { get; set; }
        public double DicountRate { get; set; }

        public List<Customer>? Customers { get; set; }

    }
}
