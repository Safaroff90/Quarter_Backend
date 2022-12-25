using Microsoft.AspNetCore.Identity;

namespace Quarter.Models
{
    public class AppUser:IdentityUser
    {
        
        public string Fullname { get; set; }
        public List<WishlistItem> WishlistItems { get; set; }
    }
}
