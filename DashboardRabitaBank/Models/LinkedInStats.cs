using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DashboardRabitaBank.Models
{
    [BsonIgnoreExtraElements]
    public class LinkedInStats
    {
        [BsonElement("total_reactions")]
        [BsonRepresentation(BsonType.Int32)]
        public int TotalReactions { get; set; }

        [BsonElement("like")]
        [BsonRepresentation(BsonType.Int32)]
        public int Like { get; set; }

        [BsonElement("love")]
        [BsonRepresentation(BsonType.Int32)]
        public int Love { get; set; }

        [BsonElement("celebrate")]
        [BsonRepresentation(BsonType.Int32)]
        public int Celebrate { get; set; }

        [BsonElement("reposts")]
        [BsonRepresentation(BsonType.Int32)]
        public int Reposts { get; set; }
    }
}