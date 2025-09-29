using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DashboardRabitaBank.Models
{
    [BsonIgnoreExtraElements]
    public class LinkedInMediaItem
    {
        [BsonElement("url")]
        public string Url { get; set; }

        [BsonElement("width")]
        [BsonRepresentation(BsonType.Int32)]
        public int Width { get; set; }

        [BsonElement("height")]
        [BsonRepresentation(BsonType.Int32)]
        public int Height { get; set; }
        [BsonElement("type")]
        public string Type { get; set; }
    }
}
