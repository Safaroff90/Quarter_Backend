using Quarter.Attributes.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quarter.Models
{
    public class Feature
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string? Icon { get; set; }
        [MaxLength(200)]
        public string Title { get; set; }
        [MaxLength(200)]
        public string Desc { get; set; }
        [NotMapped]
        [MaxFileSize(2)]
        [AllowedFileTypes("image/png", "image/jpeg")]
        public IFormFile? ImageFile { get; set; }
    }
}
