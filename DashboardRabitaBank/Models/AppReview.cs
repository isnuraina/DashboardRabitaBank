using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DashboardRabitaBank.Models
{
    [BsonIgnoreExtraElements]
    public class AppReview
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string ReviewId { get; set; }          

        [BsonElement("user")]
        public string UserName { get; set; }          

        [BsonElement("userImage")]
        public string UserImage { get; set; }        

        [BsonElement("text")]
        public string Content { get; set; }          

        [BsonElement("rating")]
        public string Rating { get; set; }           

        [BsonElement("date")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime Date { get; set; }           

        [BsonElement("analysis")]
        public Analysis Analysis { get; set; }        
        [BsonIgnore]
        public int Score => ParseRating(Rating);

        private int ParseRating(string rating)
        {
            if (string.IsNullOrEmpty(rating))
                return 0;

            
            var parts = rating.Split(' ');
            if (int.TryParse(parts[0], out int score))
                return score;

            return 0;
        }
    }
}
