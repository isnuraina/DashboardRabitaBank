using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DashboardRabitaBank.Models
{
    public class GoogleReview
    {
      
            [BsonId]
            public string Id { get; set; }

            [BsonElement("content")]
            public string Content { get; set; }

            [BsonElement("userName")]
            public string UserName { get; set; }

            [BsonElement("userImage")]
            public string UserImage { get; set; }

            [BsonElement("score")]
            public int Score { get; set; }

            [BsonElement("at")]
            public DateTime At { get; set; }

            [BsonElement("analysis")]
            public CommentAnalysis Analysis { get; set; }
        
    }
}
