using Microsoft.EntityFrameworkCore;
using MvcWebApp.Entities;
using MvcWebApp.Models.ViewModels;

namespace MvcWebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public virtual DbSet<Friend> Friends { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<FriendFriends> FriendFriends { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Friend>()
                .HasOne(e => e.Country)
                .WithMany(a => a.Friend)
                .HasForeignKey(a => a.CountryId);

            modelBuilder.Entity<FriendFriends>()
                .HasKey(ff => new { ff.FriendId, ff.UserId });

            modelBuilder.Entity<FriendFriends>()
                .HasOne(a => a.Friend)
                .WithMany(a => a.ContactFrom)
                .HasForeignKey(a => a.FriendId)
                .OnDelete(DeleteBehavior.NoAction);
            //.OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FriendFriends>()
                .HasOne(a => a.User)
                .WithMany(a => a.ContactTo)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<Friend>()
            //    .HasMany(a => a.ContactTo)                
            //    .WithOne(a => a.User)
            //    .HasForeignKey(a => a.UserId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Friend>()
            //    .HasMany(a => a.ContactFrom)
            //    .WithOne(a => a.Friend)
            //    .HasForeignKey(a => a.FriendId)
            //    .OnDelete(DeleteBehavior.Cascade);
                
        }
    }
}
