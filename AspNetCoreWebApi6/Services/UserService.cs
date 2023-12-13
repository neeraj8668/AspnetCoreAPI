

using AspNetCoreWebApi6.Models.Dto;
using AspNetCoreWebApi6.Options;

namespace AspNetCoreWebApi6.Services
{
    public class UserService : IUserSevice
    {
        private readonly IMongoCollection<Product> _productsCollection;
        private readonly IMongoCollection<User> _userCollection;
        private readonly SampleDatabaseSettings _dbSettings;
        private readonly IMongoClient _mongoClient;

        public UserService(IOptions<SampleDatabaseSettings> dbSettings, IMongoClient mongoClient)
        {
            _dbSettings=dbSettings.Value;

            _mongoClient= mongoClient;

            var mongoDatabase = _mongoClient.GetDatabase(_dbSettings.DatabaseName);

            _userCollection = mongoDatabase.GetCollection<User>(_dbSettings.UserCollectionName);
            _productsCollection  = mongoDatabase.GetCollection<Product>(_dbSettings.ProductCollectionName);
        }
        public async Task<IEnumerable<User>> GetUsersWithProductsAsync()
        {
            return await _userCollection.Find(_ => true).ToListAsync();
            //var usersWithProducts = await _userCollection
            //    .Aggregate()
            //    .Lookup<User, Product, UserWithProducts>(
            //        _productsCollection,
            //        user => user.ProductIds,
            //        product => product.Id,
            //        userWithProducts => userWithProducts.Products)  
            //    .ToListAsync();

            //return usersWithProducts;
        }
        public async Task<List<User>> GetAllAsync() =>
            await _userCollection.Find(_ => true).ToListAsync();


        public async Task<User?> GetAsync(string id)
        {
            return await _userCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

            //.Aggregate()
            //.Match(user => user.Id == id)
            //.Lookup<User, Product, UserWithProducts>(
            //    _productsCollection,
            //    user => user.ProductIds,
            //    product => product.Id,
            //    userWithProducts => userWithProducts.Products)
            //.FirstOrDefaultAsync();

            // return userWithProducts;
        }

        public async Task CreateAsync(User newBook) =>
            await _userCollection.InsertOneAsync(newBook);

        public async Task UpdateAsync(string id, User updatedBook) =>
            await _userCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

        public async Task RemoveAsync(string id) =>
            await _userCollection.DeleteOneAsync(x => x.Id == id);
    }

}
