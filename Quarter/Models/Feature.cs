using System.ComponentModel.DataAnnotations;

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

    }
}
