using Quarter.Models;

namespace Quarter.ViewModels
{
    public class HomeViewModel
    {
        public List<Slider> Sliders { get; set; }
        public List<Feature> Features { get; set; }
        public List<House> Houses { get; set; }
        public Dictionary<string, string> Settings { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Comment> Comments { get; set; }
        public List<AppUser> AppUsers { get; set; }

    }
}
