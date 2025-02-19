using System.ComponentModel.DataAnnotations;

namespace AlyssaVideoShopRentalAPI.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
       
        public string? LastName { get; set; }

        public string? FirstName { get; set; }

        public DateOnly BirthDate { get; set; }

        public string? Email { get; set; }


        public string? Address { get; set; }


        public ICollection<RentalHeader>? RentalHeaders { get; set; }
    }
}

