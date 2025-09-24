using MongoDB.Bson.Serialization.Attributes;

namespace DashboardRabitaBank.Models
{
    [BsonIgnoreExtraElements]
    public class FacebookUser
    {
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("profileUrl")]
        public string ProfileUrl { get; set; }

        [BsonElement("profilePic")]
        public string ProfilePic { get; set; }
    }
}
