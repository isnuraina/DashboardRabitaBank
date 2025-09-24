using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DashboardRabitaBank.Models
{
    [BsonIgnoreExtraElements]
    public class FacebookMedia
    {
        [BsonElement("thumbnail")]
        public string Thumbnail { get; set; }

        [BsonElement("photo_image")]
        public FacebookPhotoImage PhotoImage { get; set; }

        [BsonElement("url")]
        public string Url { get; set; }

        [BsonElement("id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string MediaId { get; set; }

        [BsonElement("ocrText")]
        public string OcrText { get; set; }
    }
}
