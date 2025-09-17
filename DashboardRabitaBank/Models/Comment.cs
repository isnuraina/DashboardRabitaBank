using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DashboardRabitaBank.Models
{
    public class Comment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("commentText")]
        public string CommentText { get; set; }

        [BsonElement("commentatorUserName")]
        public string CommentatorUserName { get; set; }

        [BsonElement("commentatorProfilePicUrl")]
        public string CommentatorProfilePicUrl { get; set; }

        [BsonElement("analysis")]
        public CommentAnalysis Analysis { get; set; }
    }
}
