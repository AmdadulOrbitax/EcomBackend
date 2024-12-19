using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace E_commerce.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } = string.Empty;
        public string UserName { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; }
        public string Roles { get; set; }
    }
}
