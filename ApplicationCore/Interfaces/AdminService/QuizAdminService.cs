using ApplicationCore.Commons.Repository;
using ApplicationCore.Models.QuizAggregate;

namespace BackendLab01;

public class QuizAdminService:IQuizAdminService
{
    private readonly IGenericRepository<Quiz, int> _quizRepository;
    private readonly IGenericRepository<QuizItem, int> _itemRepository;

    public QuizAdminService(IGenericRepository<Quiz, int> quizRepository, IGenericRepository<QuizItem, int> itemRepository)
    {
        this._quizRepository = quizRepository;
        this._itemRepository = itemRepository;
    }

    public QuizItem AddQuizItem(string question, List<string> incorrectAnswers, string correctAnswer, int points)
    {
        return _itemRepository.Add(new QuizItem(question: question, incorrectAnswers: incorrectAnswers, correctAnswer: correctAnswer, id: 0));
    }

    public void UpdateQuizItem(int id, string question, List<string> incorrectAnswers, string correctAnswer, int points)
    {
        var quizItem = new QuizItem(id: id, question: question, incorrectAnswers: incorrectAnswers, correctAnswer: correctAnswer);
        _itemRepository.Update(id, quizItem);
    }

    public Quiz AddQuiz(string title, List<QuizItem> items)
    {
        return _quizRepository.Add(new Quiz( 0, title: title, items: items));
    }

    public List<QuizItem> FindAllQuizItems()
    {
        return _itemRepository.FindAll();
    }

    public List<Quiz> FindAllQuizzes()
    { return _quizRepository.FindAll();
    }
}