namespace AlyssaVideoShopRentalAPI.Models
{
    public class RentalHeader
    {
        public int RentalHeaderId { get; set; }
        public int CustomerId { get; set; }
        public int MovieId { get; set; }
        public Customer? Customer { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
        
        public ICollection<RentalDetail>? RentalDetails { get; set; }
    }
}
