using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace XYZ.Models
{
    public class CategoriesModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Category { get; set; }
    }
}
