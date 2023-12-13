using MongoDB.Bson;

namespace AspNetCoreWebApi6.Models.Dto
{
    public class UserWithProducts :User 
    { 
        public List<Product> Products { get; set; }
    }
}
