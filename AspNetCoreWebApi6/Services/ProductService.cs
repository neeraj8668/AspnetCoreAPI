

using AspNetCoreWebApi6.Options;

namespace AspNetCoreWebApi6.Services
{
    public class ProductService : IProductSevice
    {
        private readonly IMongoCollection<Product> _ProductCollection;
        private readonly SampleDatabaseSettings _dbSettings;
        private readonly IMongoClient _mongoClient;

        public ProductService(IOptions<SampleDatabaseSettings> dbSettings , IMongoClient mongoClient)
        {
            _dbSettings=dbSettings.Value;

            _mongoClient= mongoClient;

            var mongoDatabase = _mongoClient.GetDatabase(_dbSettings.DatabaseName);

            _ProductCollection = mongoDatabase.GetCollection<Product>(_dbSettings.ProductCollectionName);
        }

        public async Task<List<Product>> GetAllAsync() =>
            await _ProductCollection.Find(_ => true).ToListAsync();

        public async Task<Product?> GetAsync(string id) =>
            await _ProductCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Product newBook) =>
            await _ProductCollection.InsertOneAsync(newBook);

        public async Task UpdateAsync(string id, Product updatedBook) =>
            await _ProductCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

        public async Task RemoveAsync(string id) =>
            await _ProductCollection.DeleteOneAsync(x => x.Id == id);
    }

}
