using Quarter.Attributes.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quarter.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(150)]
        public string Subtitle { get; set; }
        [MaxLength(100)]
        public string Image { get; set; }
        [MaxLength(250)]
        public string Desc { get; set; }
        [MaxLength(50)]
        public string BtnText1 { get; set; }
        [MaxLength(50)]
        public string? BtnText2 { get; set; }
        [MaxLength(150)]
        public string? BtnUrl { get; set; }
        [MaxLength(150)]
        public string? BtnUrl2 { get; set; }
        public int Order { get; set; }
        [MaxLength(100)]
        public string? Icon { get; set; }

        [NotMapped]
        [MaxFileSize(2)]
        [AllowedFileTypes("image/png", "image/jpeg")]
        public IFormFile ImageFile { get; set; }
    }
}
