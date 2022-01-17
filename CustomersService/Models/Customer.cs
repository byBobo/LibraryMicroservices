using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CustomersService.Models
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Name")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
