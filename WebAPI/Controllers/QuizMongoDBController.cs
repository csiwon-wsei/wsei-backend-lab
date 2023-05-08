using ApplicationCore.Models;
using Infrastructure.MongoDB;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("/api/v2/quizzes")]
public class QuizMongoDBController: ControllerBase
{
    private readonly QuizUserServiceMongoDB _service;

    public QuizMongoDBController(QuizUserServiceMongoDB service)
    {
        _service = service;
    }

    [HttpGet]
    public IEnumerable<Quiz> getAllQuizzes()
    {
        return _service.FindAllQuizzes();
    }

}