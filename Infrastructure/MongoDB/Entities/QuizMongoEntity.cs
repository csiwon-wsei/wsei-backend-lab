using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.MongoDB;

public class QuizMongoEntity: BaseMongoEntity
{
    [BsonElement("id")]
    public int QuizId { get; set; }
    
    [BsonElement("title")]
    public String Title { get; set; }
    
    [BsonElement("items")]
    public List<QuizItemMongoEntity> Items { get; set; }
}