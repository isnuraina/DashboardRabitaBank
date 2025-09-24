using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DashboardRabitaBank.Models
{
    [BsonIgnoreExtraElements]
    public class LinkedInAuthor
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("follower_count")]
        [BsonRepresentation(BsonType.Int32)]
        public int FollowerCount { get; set; }

        [BsonElement("company_url")]
        public string CompanyUrl { get; set; }

        [BsonElement("logo_url")]
        public string LogoUrl { get; set; }
    }
}
