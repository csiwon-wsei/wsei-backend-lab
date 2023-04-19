using ApplicationCore.Models;
using Infrastructure.EF.Entities;

namespace Infrastructure.EF.Mappers;

public static class QuizMapper 
{
    public static Quiz FromEntityToQuiz(QuizEntity? entity)
    {
        return new Quiz(
            entity.Id,
            entity.Items.Select(FromEntityToQuizItem).ToList(),
            entity.Title
        );
    }

    public static QuizItem FromEntityToQuizItem(QuizItemEntity? entity)
    {
        return new QuizItem(
            entity.Id,
            entity.Question,
            entity.IncorrectAnswers.Select(e => e.Answer).ToList(),
            entity.CorrectAnswer);
    }
}