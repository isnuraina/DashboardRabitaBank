using MongoDB.Bson.Serialization.Attributes;

namespace DashboardRabitaBank.Models
{
    [BsonIgnoreExtraElements]
    public class LinkedInEngagement
    {
        [BsonElement("likes")]
        public int Likes { get; set; }

        [BsonElement("comments")]
        public int Comments { get; set; }
    }

}
