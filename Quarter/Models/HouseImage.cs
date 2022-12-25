using System.ComponentModel.DataAnnotations;

namespace Quarter.Models
{
    public class HouseImage
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Image { get; set; }
        public int HouseId { get; set; }
        public bool? PosterStatus { get; set; }

        public House House { get; set; }
    }
}
