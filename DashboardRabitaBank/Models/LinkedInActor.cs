using MongoDB.Bson.Serialization.Attributes;

namespace DashboardRabitaBank.Models
{
    [BsonIgnoreExtraElements]
    public class LinkedInActor
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("position")]
        public string Position { get; set; }

        [BsonElement("linkedinUrl")]
        public string LinkedinUrl { get; set; }
    }
}
