using Microsoft.EntityFrameworkCore;
using Polly;
using System.Xml;
using static Azure.Core.HttpHeader;

namespace Catalog.API.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }

     
    }
}
