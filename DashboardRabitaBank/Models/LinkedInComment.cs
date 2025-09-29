using MongoDB.Bson.Serialization.Attributes;

namespace DashboardRabitaBank.Models
{
    public class LinkedInComment
    {
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("_id")]
        [BsonIgnoreIfDefault]
        public string _Id { get; set; }

        [BsonElement("author")]
        public string Author { get; set; }

        [BsonElement("text")]
        public string Text { get; set; }

        [BsonElement("posted_at")]
        public string PostedAt { get; set; }

        [BsonElement("post_url")]
        public string PostUrl { get; set; }

        [BsonElement("analysis")]
        public Analysis Analysis { get; set; }
    }
}
