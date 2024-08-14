namespace Catalog.API.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (Products != null)
            {
                return;
            }
            modelBuilder.Entity<Product>()
                .HasData(
                    new Product
                    {
                        Id = new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"),
                        Name = "IPhone X",
                        Description = "this phone is a new version of iphone",
                        ImageFile = "product-1.png",
                        Price=950,
                        Category= "2"
                    }
                );
        }

    }
}
