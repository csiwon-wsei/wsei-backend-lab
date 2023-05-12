

using ApplicationCore.Interfaces.Criteria;
using ApplicationCore.Models;

namespace ApplicationCore.Specifications;

public class QuizItemByQuestion: BaseSpecification<QuizItem>
{
    public QuizItemByQuestion(string question): base(item => item.Question == question)
    {
        AddInclude(item => item.IncorrectAnswers);
    }
}