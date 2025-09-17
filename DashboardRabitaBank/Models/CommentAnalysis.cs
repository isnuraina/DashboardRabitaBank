using MongoDB.Bson.Serialization.Attributes;

namespace DashboardRabitaBank.Models
{
    public class CommentAnalysis
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
        public List<string> Categories { get; set; }

        [BsonElement("processedAt")]
        public DateTime ProcessedAt { get; set; }
    }
}
