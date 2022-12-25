using System.ComponentModel.DataAnnotations;

namespace Quarter.Models
{
    public class Tag
    {
        public int Id { get; set; }
        [MaxLength(25)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Icon { get; set; }
        public bool IsSelected { get; set; }

        public List<HouseTag> HouseTags { get; set; }
    }
}
