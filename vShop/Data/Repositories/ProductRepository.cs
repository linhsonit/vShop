using ProductService.Data.Model;

namespace ProductService.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _dbContext;
        public ProductRepository(ProductDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateProductAsync(Product customer)
        {
            var entity = _dbContext.Set<Product>().Add(customer);
            await _dbContext.SaveChangesAsync();

            return entity.Entity.Id;
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return new Product()
            {
                ProductName = "Iphone 16",
                Descriptions = "Release 2024",
                Version = "1.0"
            };
        }
    }
}
