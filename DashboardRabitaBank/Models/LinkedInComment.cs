using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DashboardRabitaBank.Models
{
    [BsonIgnoreExtraElements]
    public class LinkedInComment
    {
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("commentary")]
        public string Commentary { get; set; }

        [BsonElement("createdAt")]
        public string CreatedAt { get; set; }

        [BsonElement("actor")]
        public LinkedInActor Actor { get; set; }

        [BsonElement("analysis")]
        public LinkedInAnalysis Analysis { get; set; }

        [BsonElement("linkedinUrl")]
        public string LinkedinUrl { get; set; }

        [BsonElement("engagement")]
        public LinkedInEngagement Engagement { get; set; }
    }
}