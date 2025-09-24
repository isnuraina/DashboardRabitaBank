using MongoDB.Bson.Serialization.Attributes;

namespace DashboardRabitaBank.Models
{
    [BsonIgnoreExtraElements]
    public class FacebookTextReference
    {
        [BsonElement("url")]
        public string Url { get; set; }

        [BsonElement("external_url")]
        public string ExternalUrl { get; set; }

        [BsonElement("mobileUrl")]
        public string MobileUrl { get; set; }

        [BsonElement("id")]
        public string RefId { get; set; }
    }
}
