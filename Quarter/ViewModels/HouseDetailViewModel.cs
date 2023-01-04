using Quarter.Models;

namespace Quarter.ViewModels
{
    public class HouseDetailViewModel
    {

        public House House { get; set; }
        public List<House> RelatedHouses { get; set; }
        public CommentCreateViewModel CommentVM { get; set; }
        public bool HasComment { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Realtor> Realtors { get; set; }
        public List<Category> Categories { get; set; }
        public List<House> Houses { get; set; }
    }
}
