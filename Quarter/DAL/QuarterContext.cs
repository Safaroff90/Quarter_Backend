using Microsoft.EntityFrameworkCore;
using Quarter.Models;

namespace Quarter.DAL
{
    public class QuarterContext:DbContext
    {
        public QuarterContext(DbContextOptions<QuarterContext> options): base(options)
        {

        }

        public DbSet<House> Houses { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Realtor> Realtors { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<HouseImage> HousesImages { get; set; }
        public DbSet<HouseTag> HouseTags { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

    }
}
