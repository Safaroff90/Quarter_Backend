using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Quarter.Attributes.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quarter.Models
{
    public class House
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(4);
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow.AddHours(4);
        [MaxLength(300)]
        public string Desc { get; set; }
        public int BedroomCount { get; set; }
        public int BathroomCount { get; set; }
        public int CarParkingCount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal SalePrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal CostPrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountPercent { get; set; }
        public int RealtorId { get; set; }  
        public int AreaId { get; set; }  
        public int CategoryId { get; set; }  
        public bool IsFeatured { get; set; }
        public bool IsSold { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal SquareFt { get; set; }
        public bool IsNew { get; set; }
        [NotMapped]
        [MaxFileSize(2)]
        [AllowedFileTypes("image/jpeg", "image/png")]
        public IFormFile? PosterFile { get; set; }
        [NotMapped]
        [MaxFileSize(2)]
        public List<IFormFile>? ImageFiles { get; set; } = new List<IFormFile>();
        [NotMapped]
        public List<int>? HouseImageIds { get; set; } = new List<int>();
        [NotMapped]
        public List<int>? TagIds { get; set; } = new List<int>();
        public List<Comment> Comments { get; set; } 
        public Realtor Realtor { get; set; }
        public Area Area { get; set; }
        public Category Category { get; set; }          
        public List<HouseImage> HouseImages { get; set; }
        public List<HouseTag> HouseTags { get; set; }
        
    }
}
