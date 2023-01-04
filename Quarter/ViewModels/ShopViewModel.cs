using Quarter.Models;

namespace Quarter.ViewModels
{
    public class ShopViewModel
    {
        public PaginatedList<House>? Houses { get; set; }
        public List<Category>? Categories { get; set; }
        public List<Tag>? Tags { get; set; }
        public List<Realtor>? Realtors { get; set; }
        public List<Area>? Areas { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal MinPrice { get; set; }
    }
}
