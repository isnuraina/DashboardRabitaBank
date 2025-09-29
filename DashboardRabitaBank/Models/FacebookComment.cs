using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DashboardRabitaBank.Models
{
    public class FacebookComment
    {
        [BsonElement("_id")]
        [BsonIgnoreIfDefault]
        public string _Id { get; set; }

        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("userName")]
        public string UserName { get; set; }

        [BsonElement("commentText")]
        public string CommentText { get; set; }

        [BsonElement("commentDate")]
        public string CommentDate { get; set; }

        [BsonElement("facebookUrl")]
        public string FacebookUrl { get; set; }

        [BsonElement("postUrl")]
        public string PostUrl { get; set; }

        [BsonElement("analysis")]
        public Analysis Analysis { get; set; }

        [BsonElement("reactionCount")]
        public string ReactionCount { get; set; }

        [BsonElement("userProfileImageUrl")]
        public string UserProfileImageUrl { get; set; }

        [BsonElement("feedbackId")]
        public string FeedbackId { get; set; }

        [BsonElement("userGender")]
        public string UserGender { get; set; }

        [BsonElement("userUrl")]
        public string UserUrl { get; set; }

        [BsonElement("userId")]
        public string UserId { get; set; }

        [BsonElement("userIsVerified")]
        public bool UserIsVerified { get; set; }

        [BsonElement("userSubscribeStatus")]
        public string UserSubscribeStatus { get; set; }

        [BsonElement("_scrapedAt")]
        public string ScrapedAt { get; set; }
    }
}
