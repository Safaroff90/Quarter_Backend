using System.ComponentModel.DataAnnotations;

namespace Quarter.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int HouseId { get; set; }
        public int AppUserId { get; set; }
        [MaxLength(150)]
        public string Text { get; set; }

        public House House { get; set; }
        public AppUser AppUser { get; set; }
    }
}
