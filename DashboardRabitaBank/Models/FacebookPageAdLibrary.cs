using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DashboardRabitaBank.Models
{
    [BsonIgnoreExtraElements]
    public class FacebookPageAdLibrary
    {
        [BsonElement("is_business_page_active")]
        public bool IsBusinessPageActive { get; set; }

        [BsonElement("id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

    }
}
