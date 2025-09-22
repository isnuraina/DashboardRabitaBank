using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DashboardRabitaBank.Models
{
    [BsonIgnoreExtraElements]
    public class GoogleReview
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string ReviewId { get; set; }

        [BsonElement("userName")]
        public string UserName { get; set; }

        [BsonElement("userImage")]
        public string UserImage { get; set; }

        [BsonElement("content")]
        public string Content { get; set; }

        [BsonElement("score")]
        [BsonRepresentation(BsonType.Int32)]
        public int Score { get; set; }

        [BsonElement("at")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime At { get; set; }

        [BsonElement("analysis")]
        public Analysis Analysis { get; set; }
    }


}
