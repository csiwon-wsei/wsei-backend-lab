namespace Infrastructure.MongoDB;

public class MongoDBSettings
{
    public string ConnectionUri { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string QuizCollection { get; set; } = null!;

    public string ConnectionUriLocal { get; set; } = null!;
}