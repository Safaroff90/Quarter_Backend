using System.ComponentModel.DataAnnotations;

namespace Quarter.Models
{
    public class Area
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public List<House> Houses { get; set; }
    }
}
