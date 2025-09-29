using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DashboardRabitaBank.Models
{
    [BsonIgnoreExtraElements]
    public class LinkedInPost
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }  // _id və ya full_urn

        [BsonElement("full_urn")]
        public string FullUrn { get; set; }

        [BsonElement("text")]
        public string Text { get; set; }

        [BsonElement("post_url")]
        public string PostUrl { get; set; }

        [BsonElement("post_type")]
        public string PostType { get; set; }

        [BsonElement("post_language_code")]
        public string PostLanguageCode { get; set; }

        [BsonElement("posted_at")]
        public LinkedInPostedAt PostedAt { get; set; }

        [BsonElement("author")]
        public LinkedInAuthor Author { get; set; }

        [BsonElement("media")]
        public LinkedInMedia Media { get; set; }

        [BsonElement("source_company")]
        public string SourceCompany { get; set; }

        [BsonElement("stats")]
        public LinkedInStats Stats { get; set; }
    }
}