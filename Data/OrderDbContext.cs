using Microsoft.EntityFrameworkCore;
using OrderAPI.Models;
using OrderAPI.ViewModels;

namespace OrderAPI.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options)
            : base(options) { }

        public DbSet<DishModel> Dishes { get; set; }
        public DbSet<OrderModel> Orders { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<OrderedDishViewModel>().HasOne(od => od.Order).WithMany(o => o.OrderedDishes).HasForeignKey(od => od.OrderId);
        //}
    }
}
