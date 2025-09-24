using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DashboardRabitaBank.Models
{
    [BsonIgnoreExtraElements] // JSON-da əlavə sahələr olsa error atmasın
    public class FacebookPost
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; }

        [BsonElement("_scrapedAt")]
        public DateTime ScrapedAt { get; set; }

        [BsonElement("facebookId")]
        public string FacebookId { get; set; }

        [BsonElement("facebookUrl")]
        public string FacebookUrl { get; set; }

        [BsonElement("feedbackId")]
        public string FeedbackId { get; set; }

        [BsonElement("inputUrl")]
        public string InputUrl { get; set; }

        [BsonElement("likes")]
        public int Likes { get; set; }

        [BsonElement("link")]
        public string Link { get; set; }

        [BsonElement("media")]
        public List<FacebookMedia> Media { get; set; }

        [BsonElement("pageAdLibrary")]
        public FacebookPageAdLibrary PageAdLibrary { get; set; }

        [BsonElement("pageName")]
        public string PageName { get; set; }

        [BsonElement("postId")]
        public string PostId { get; set; }

        [BsonElement("shares")]
        public int Shares { get; set; }

        [BsonElement("text")]
        public string Text { get; set; }

        [BsonElement("textReferences")]
        public List<FacebookTextReference> TextReferences { get; set; }

        [BsonElement("time")]
        public DateTime Time { get; set; }

        [BsonElement("timestamp")]
        public long Timestamp { get; set; }

        [BsonElement("topLevelUrl")]
        public string TopLevelUrl { get; set; }

        [BsonElement("topReactionsCount")]
        public int TopReactionsCount { get; set; }

        [BsonElement("url")]
        public string Url { get; set; }

        [BsonElement("user")]
        public FacebookUser User { get; set; }
    }
}
