using MongoDB.Bson.Serialization.Attributes;

namespace DashboardRabitaBank.Models
{
    [BsonIgnoreExtraElements]
    public class LinkedInMedia
    {
        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("items")]
        public List<LinkedInMediaItem> Items { get; set; }
    }
}
