using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DashboardRabitaBank.Models
{
    [BsonIgnoreExtraElements] 

    public class PostInfo
    {
        [BsonElement("id")]
        public string PostId { get; set; }

        [BsonElement("caption")]
        public string Caption { get; set; }

        [BsonElement("likesCount")]
        public int LikesCount { get; set; }

        [BsonElement("timestamp")]
        public DateTime Timestamp { get; set; }

        [BsonElement("ownerFullName")]
        public string OwnerFullName { get; set; }

        [BsonElement("ownerUsername")]
        public string OwnerUsername { get; set; }

        [BsonElement("displayUrl")]
        public string DisplayUrl { get; set; }
    }
}
