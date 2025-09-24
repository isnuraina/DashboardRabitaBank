using MongoDB.Bson.Serialization.Attributes;

namespace DashboardRabitaBank.Models
{
    [BsonIgnoreExtraElements]
    public class FacebookPhotoImage
    {
        [BsonElement("uri")]
        public string Uri { get; set; }

        [BsonElement("height")]
        public int Height { get; set; }

        [BsonElement("width")]
        public int Width { get; set; }
    }
}
