
using ProductService.Data.Model;

namespace ProductService.Data.Repositories
{
    public interface IProductRepository
    {
        Task<int> CreateProductAsync(Product customer);

        Task<Product> GetProductAsync(int id);
    }
}
