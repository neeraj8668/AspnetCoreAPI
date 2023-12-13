using AspNetCoreWebApi6.Models;
using AspNetCoreWebApi6.Models.Dto;

namespace AspNetCoreWebApi6.Services
{
    public interface IUserSevice
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetAsync(string id);
        Task UpdateAsync(string id, User updatedBook);
        Task RemoveAsync(string id);
        Task CreateAsync(User newUser);
        Task<IEnumerable<User>> GetUsersWithProductsAsync();
    } 

}
