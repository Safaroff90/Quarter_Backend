using Quarter.Models;

namespace Quarter.ViewModels
{
    public class WishListItemViewModel
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public House House { get; set; }
    }
}
