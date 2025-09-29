using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DashboardRabitaBank.Models
{
    [BsonIgnoreExtraElements]
    public class LinkedInPostedAt
    {
        [BsonElement("relative")]
        public string Relative { get; set; }

        [BsonElement("is_edited")]
        public bool IsEdited { get; set; }

        [BsonElement("date")]
        public string Date { get; set; }

        [BsonElement("timestamp")]
        [BsonRepresentation(BsonType.Int64)]
        public long Timestamp { get; set; }
    }
}