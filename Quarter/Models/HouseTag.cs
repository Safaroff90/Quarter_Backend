namespace Quarter.Models
{
    public class HouseTag
    {  
        public int Id { get; set; }
        public int HouseId { get; set; }
        public int TagId { get; set; }


        public House House { get; set; }
        public Tag Tag { get; set; }
    }
}
