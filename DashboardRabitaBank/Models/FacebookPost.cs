using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DashboardRabitaBank.Models
{
    [BsonIgnoreExtraElements] // JSON-da əlavə sahələr olsa error atmasın
    public class FacebookPost
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string _id { get; set; }

        [BsonElement("text")]
        public string Text { get; set; }

        [BsonElement("postId")]
        public string PostId { get; set; }

        [BsonElement("url")]
        public string Url { get; set; }

        [BsonElement("time")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime Time { get; set; }

        [BsonElement("likes")]
        public BsonValue Likes { get; set; }

        [BsonElement("shares")]
        public BsonValue Shares { get; set; }

        [BsonElement("viewsCount")]
        public BsonValue ViewsCount { get; set; }

        [BsonElement("comments")]
        public FacebookComment[] Comments { get; set; }

        [BsonElement("pageName")]
        public string PageName { get; set; }

        [BsonElement("topLevelUrl")]
        public string TopLevelUrl { get; set; }

        [BsonElement("facebookUrl")]
        public string FacebookUrl { get; set; }

        [BsonElement("_scrapedAt")]
        public string ScrapedAt { get; set; }
    }
}
