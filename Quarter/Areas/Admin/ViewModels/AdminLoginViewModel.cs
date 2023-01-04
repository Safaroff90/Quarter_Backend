using System.ComponentModel.DataAnnotations;

namespace Quarter.Areas.Admin.ViewModels
{
    public class AdminLoginViewModel
    {
        [MaxLength(25)]
        public string Username { get; set; }
        [MaxLength(20)]
        public string Password { get; set; }
        public bool IsPersist { get; set; }
    }
}
