using Ordering.Application.Data;
using Ordering.Domain.Models;
using System.Reflection;
namespace Ordering.Infrastucture.Data
{
    public class ApplicationDbContext :DbContext,IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) { }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder Builder)
        {
            Builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(Builder);
        }
    }
}
