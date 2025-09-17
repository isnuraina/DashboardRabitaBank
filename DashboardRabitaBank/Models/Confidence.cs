using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DashboardRabitaBank.Models
{
    public class Confidence
    {
        [BsonRepresentation(BsonType.Double)]
        [BsonElement("$numberDouble")]
        public double Number { get; set; }
    }
}
