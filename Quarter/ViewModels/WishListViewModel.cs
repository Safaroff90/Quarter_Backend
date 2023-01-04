namespace Quarter.ViewModels
{
    public class WishListViewModel
    {
        public List<WishListItemViewModel> Items { get; set; } = new List<WishListItemViewModel>();
        public decimal TotalPrice { get; set; }
    }
}
