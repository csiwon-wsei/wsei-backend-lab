using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Entities;
[PrimaryKey(nameof(UserId), nameof(QuizId), nameof(QuizId))]
public class QuizItemUserAnswerEntity
{
    public int UserId { get; set; }
    public int QuizItemId { get; set; }
    public int QuizId { get; set; }
    public string UserAnswer { get; set; }

    public ISet<QuizItemEntity> QuizItems { get; set; } = new HashSet<QuizItemEntity>();
}