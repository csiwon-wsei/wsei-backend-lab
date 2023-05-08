using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.MongoDB;

public class QuizItemMongoEntity
{
    [BsonElement("id")]
    public int ItemId { get; set; }
    
    [BsonElement("question")]
    public string Question { get; set; }
    
    [BsonElement("incorrectAnswers")]
    public List<string> IncorrectAnswers { get; set; }
    
    [BsonElement("correctAnswer")]
    public string CorrectAnswer { get; set; }
}