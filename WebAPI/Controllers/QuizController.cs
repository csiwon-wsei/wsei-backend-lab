using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using Web.Services;
using WebAPI.Dto;

namespace WebAPI.Controllers;
[ApiController]
[Route("/api/v1/quizzes")]
public class QuizController: ControllerBase
{
    private readonly IQuizUserService _service;
    private readonly IMessageProducer _producer;

    public QuizController(IQuizUserService service, IMessageProducer producer)
    {
        _service = service;
        _producer = producer;
    }
    [HttpGet]
    [Route("{id}")]
    public ActionResult<QuizDto> FindById(int id)
    {
        var result = QuizDto.of(_service.FindQuizById(id));
        return result is null ?  NotFound() : Ok(result);
    }

    [HttpGet]
    public IEnumerable<QuizDto> FindAll()
    {
        return _service.FindAllQuizzes().Select(QuizDto.of).AsEnumerable();
    }

    [HttpGet]
    [Route("{quizId}/items/{itemId}/answers")]
    public ActionResult<QuizItemAnswerDto> GetAnswer(int quizId, int itemId)
    {
        //TODO dokończyć
        return Ok();
    }

    [HttpPost]
    //[Authorize(Policy = "Bearer")]
    [Route("{quizId}/items/{itemId}/answers")]
    public ActionResult SaveAnswer([FromBody] QuizItemAnswerDto dto, LinkGenerator linker,int quizId, int itemId)
    {
        try
        {
            var answer = _service.SaveUserAnswerForQuiz(quizId, itemId, dto.UserId, dto.UserAnswer);
            _producer.SendMessage(new AnswerStatisticDto()
            {
                Answer = dto.UserAnswer,
                QuizItemId = itemId,
                isCorrect = _service.FindQuizById(quizId).Items.Find(i => i.Id == itemId).CorrectAnswer == dto.UserAnswer
            });
            //var uri = linker.GetPathByAction(HttpContext, "GetAnswer", "Quiz", (quizId, itemId)) ?? string.Empty;
            return Created("", new
            {
                QuizId = answer.QuizId,
                QuizItemId = itemId,
                dto.UserAnswer
            });
        }
        catch (Exception e)
        {
            return BadRequest(new
            {
                error = e.Message
            });
        }
    }

    [HttpGet, Produces("application/json")]
    [Route("{quizId}/feedbacks")]
    public FeedbackQuizDto GetFeedback(int quizId)
    {
        int userId = 1;
        var answers = _service.GetUserAnswersForQuiz(quizId, userId);
        //TODO: zdefiniuj mapper listy odpowiedzi na obiekt FeedbackQuizDto 
        return new FeedbackQuizDto()
        {
            QuizId = quizId,
            UserId = 1,
            QuizItemsAnswers = answers.Select(i => new FeedbackQuizItemDto()
            {
                Question = i.QuizItem.Question,
                Answer = i.Answer,
                IsCorrect = i.IsCorrect(),
                QuizItemId = i.QuizItem.Id
            }).ToList()
        };
    }
}