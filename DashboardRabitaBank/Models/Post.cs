using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DashboardRabitaBank.Models
{
    public class Post
    {
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]

        public string Id { get; set; }

        [BsonElement("postInfo")]
        public PostInfo PostInfo { get; set; }

        [BsonElement("comments")]
        public List<Comment> Comments { get; set; }
    }

}
