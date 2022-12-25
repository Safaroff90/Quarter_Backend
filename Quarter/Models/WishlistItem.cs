namespace Quarter.Models
{
    public class WishlistItem
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public int HouseId { get; set; }
        public int Count { get; set; }


        public AppUser AppUser { get; set; }
        public House House { get; set; }
    }
}
