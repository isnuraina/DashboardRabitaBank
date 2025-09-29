using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DashboardRabitaBank.Models
{
    [BsonIgnoreExtraElements]

    public class Analysis
    {
        [BsonElement("done")]
        public bool Done { get; set; }

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

        [BsonElement("relevance")]
        public double Relevance { get; set; }

        [BsonElement("categories")]
        public string[] Categories { get; set; }

        [BsonElement("processedAt")]
        public string ProcessedAt { get; set; }
    }
}
