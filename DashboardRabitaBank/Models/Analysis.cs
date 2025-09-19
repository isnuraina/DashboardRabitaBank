using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DashboardRabitaBank.Models
{
    public class Analysis
    {
        [BsonElement("sentiment")]
        public string Sentiment { get; set; }

        [BsonElement("purpose")]
        public string Purpose { get; set; }
    }
}
