using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DashboardRabitaBank.Models
{
    public class Insight
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; } 

        [BsonElement("comments")]
        public List<Comment> Comments { get; set; }

        [BsonElement("postInfo")]
        public PostInfo PostInfo { get; set; }
    }
}
