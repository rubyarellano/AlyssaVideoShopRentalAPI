namespace AlyssaVideoShopRentalAPI.Models
{
    public class RentalDetail
    {
        public int RentalDetailsId { get; set; }
        public int RentalHeadersId { get; set; }
        public int MovieId { get; set; }

        public RentalHeader? RentalHeaders { get; set; }
        public Movie? Movies { get; set; }
    }
}
