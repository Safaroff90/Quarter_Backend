using Quarter.Attributes.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quarter.Models
{
    public class Realtor
    {
        public int Id { get; set; }
        [MaxLength(25)]
        public string FullName { get; set; }
        [MaxLength(100)]
        public string Desc { get; set; } 
        
        public string? Image { get; set; }    
        public List<House> House { get; set; }
        [NotMapped]
        [MaxFileSize(2)]
        [AllowedFileTypes("image/png", "image/jpeg")]
        public IFormFile? ImageFile { get; set; }
    }
}
