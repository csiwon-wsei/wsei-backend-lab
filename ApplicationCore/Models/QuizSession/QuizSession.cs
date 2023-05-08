using ApplicationCore.Interfaces.Repository;

namespace ApplicationCore.Models.QuizSession;

public class QuizSession: IIdentity<int>
{
    public int UserId { get; init; }
    
    public int QuizId { get; init; }
    
    public DateTime StartTime { get; init; }
    
    public DateTime EndTime { get; set; }
    
    public DateTime RegisterTime { get; set; }
    
    public TimeSpan MaxDuration { get; init; }
    public int Id { get; set; }

    public IEnumerable<QuizItemAnswer> Answers => _answers.AsEnumerable();

    private readonly ISet<QuizItemAnswer> _answers = new HashSet<QuizItemAnswer>();
    

}