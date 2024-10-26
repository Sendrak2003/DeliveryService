using DeliveryService.Models;
using Microsoft.EntityFrameworkCore;

public class DeliveryContext : DbContext
{
    public DeliveryContext(DbContextOptions<DeliveryContext> options) : base(options) { }

    public DbSet<Order> Orders { get; set; }


}
