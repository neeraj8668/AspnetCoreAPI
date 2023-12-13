using AspNetCoreWebApi6.Models;

namespace AspNetCoreWebApi6.Services
{
    public interface IProductSevice
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetAsync(string id);
        Task UpdateAsync(string id, Product updatedBook);
        Task RemoveAsync(string id);
        Task CreateAsync(Product newProduct);
    } 

}
