using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Infrastructure.EF.Entities;
using Infrastructure.EF.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Services;

public class QuizUserServiceEF : IQuizUserService
{
    private readonly QuizDbContext _context;

    public QuizUserServiceEF(QuizDbContext context)
    {
        _context = context;
    }

    public Quiz CreateAndGetQuizRandom(int count)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Quiz> FindAllQuizzes()
    {
        return _context
            .Quizzes
            .AsNoTracking()
            .Include(q => q.Items)
            .ThenInclude(i => i.IncorrectAnswers)
            .Select(QuizMapper.FromEntityToQuiz)
            .ToList();
    }

    public Quiz? FindQuizById(int id)
    {
        var entity = _context
            .Quizzes
            .AsNoTracking()
            .Include(q => q.Items)
            .ThenInclude(i => i.IncorrectAnswers)
            .FirstOrDefault(e => e.Id == id);
        return entity is null ? null : QuizMapper.FromEntityToQuiz(entity);
    }

    public QuizItemUserAnswer SaveUserAnswerForQuiz(int quizId, int quizItemId, int userId, string answer)
    {
        var quizzEntity = _context.Quizzes.AsNoTracking().Include(e => e.Items).FirstOrDefault(e => e.Id == quizId);
        if (quizzEntity is null)
        {
            throw new QuizNotFoundException($"Quiz with id {quizId} not found");
        }
        var item = quizzEntity
            .Items
            .FirstOrDefault(e => e.Id == quizItemId);
        if (item is null)
        {
            throw new QuizItemNotFoundException($"Quiz item with id {quizId} not found");
        }

        QuizItemUserAnswerEntity entity = new QuizItemUserAnswerEntity()
        {
            QuizId = quizId,
            UserAnswer = answer,
            UserId = userId,
            QuizItemId = quizItemId
        };
        var savedEntity = _context.Add(entity).Entity;
        _context.SaveChanges();
        return new QuizItemUserAnswer()
        {
            QuizId = savedEntity.QuizId,
            UserId = savedEntity.UserId,
            Answer = savedEntity.UserAnswer,
            QuizItem = QuizMapper.FromEntityToQuizItem(item)
        };
    }

    public List<QuizItemUserAnswer> GetUserAnswersForQuiz(int quizId, int userId)
    {
        throw new NotImplementedException();
    }
}