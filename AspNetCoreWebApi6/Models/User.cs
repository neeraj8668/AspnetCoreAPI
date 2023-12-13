using MongoDB.Bson;

namespace AspNetCoreWebApi6.Models
{
     
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        public string Name { get; set; }

        public string Email { get; set; }
        public List<string> ProductIds { get; set; } = new List<string>();

    }

}
