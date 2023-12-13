using AspNetCoreWebApi6.Options;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AspNetCoreWebApi6.Models
{
    /// <summary>
    /// class to support Seeding of Mongo collection data
    /// </summary>
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var mongoClient = serviceProvider.GetRequiredService<IMongoClient>();
            var dbSettings = serviceProvider.GetRequiredService<IOptions<SampleDatabaseSettings>>().Value;
            var mongoDatabase = mongoClient.GetDatabase(dbSettings.DatabaseName);
            var usersCollection = mongoDatabase.GetCollection<User>(dbSettings.UserCollectionName);
            var productsCollection = mongoDatabase.GetCollection<Product>(dbSettings.ProductCollectionName);

            ConfigureMongoDBIndexes(usersCollection);

            if (usersCollection.Find(_ => true).Any())
            {
                return;   // DB has been seeded
            }
            if (productsCollection.Find(_ => true).Any())
            {
                return;   // DB has been seeded
            }
            var productId = InsertProduct(productsCollection, "Product 1");
            var productId2 = InsertProduct(productsCollection, "Product 2");

            InsertUser(usersCollection, "Amit", "amit@yopmail.com", productId);
        }
        public static void ConfigureMongoDBIndexes(IMongoCollection<User> usersCollection)
        {

            // Create an index on the ProductIds field
            var productIdsIndexModel = new CreateIndexModel<User>(Builders<User>.IndexKeys.Ascending(user => user.ProductIds));
            usersCollection.Indexes.CreateOne(productIdsIndexModel);
        
        }
        private static string InsertProduct(IMongoCollection<Product> productsCollection, string productName)
        {
            var product = new Product
            {
                Name = productName,
            };

            productsCollection.InsertOne(product);

            return product.Id;
        }

        private static void InsertUser(IMongoCollection<User> usersCollection, string userName, string email, string productId)
        {
            var user = new User
            {
                Name = userName,
                Email=email,
                ProductIds = new List<string> { productId }
            };

            usersCollection.InsertOne(user);
        }
    }
}
