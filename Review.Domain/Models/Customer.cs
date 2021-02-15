namespace Review.Domain.Models
{
    public class Customer
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public Country Country { get; set; }
    }
}