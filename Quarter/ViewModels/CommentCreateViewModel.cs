using System.ComponentModel.DataAnnotations;

namespace Quarter.ViewModels
{
    public class CommentCreateViewModel
    {
        [MaxLength(150)]
        public string Text { get; set; }
        public int HouseId { get; set; }
    }
}
