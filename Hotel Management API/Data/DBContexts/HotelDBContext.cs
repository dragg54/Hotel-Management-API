using Hotel_Management_API.Entities;
using Hotel_Management_API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Management_API.Data.DBContexts
{
    public class HotelDBContext : IdentityDbContext<User>
    {
        public HotelDBContext(DbContextOptions<HotelDBContext> options) : base(options)
        {

        }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Owner> Owners { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Hotel>()
                .HasOne(h => h.Owner) 
                .WithMany(o => o.Hotels) 
                .HasForeignKey(h => h.OwnerId) 
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
