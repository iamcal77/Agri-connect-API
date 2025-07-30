using System.Collections.Generic;
using AgriConnect.Models;
using Microsoft.EntityFrameworkCore;


namespace AgriConnect.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> UsersAgri { get; set; }
        public DbSet<Cooperative> Cooperatives { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<ExtensionPost> ExtensionPosts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cooperative>()
                .HasOne(c => c.Leader)
                .WithMany()  // or .WithOne(u => u.LeadingCooperative) if you defined inverse
                .HasForeignKey(c => c.LeaderId)
                .OnDelete(DeleteBehavior.SetNull); // optional

            modelBuilder.Entity<User>()
                .HasOne(u => u.Cooperative)
                .WithMany(c => c.Members)
                .HasForeignKey(u => u.CooperativeId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Cooperative)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CooperativeId)
                .OnDelete(DeleteBehavior.SetNull);
        }

    }
}
