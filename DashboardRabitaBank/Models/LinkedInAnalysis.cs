using MongoDB.Bson.Serialization.Attributes;

namespace DashboardRabitaBank.Models
{
    [BsonIgnoreExtraElements]
    public class LinkedInAnalysis
    {
        [BsonElement("sentiment")]
        public string Sentiment { get; set; }

        [BsonElement("importance")]
        public string Importance { get; set; }

        [BsonElement("is_alarm")]
        public bool IsAlarm { get; set; }

        [BsonElement("confidence")]
        public double Confidence { get; set; }

        [BsonElement("purpose")]
        public string Purpose { get; set; }

        [BsonElement("categories")]
        public List<string> Categories { get; set; }
    }
}
