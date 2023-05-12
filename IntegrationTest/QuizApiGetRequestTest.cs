using System.Net;
using ApplicationCore.Models;
using Infrastructure.EF;
using Infrastructure.EF.Entities;
using Infrastructure.Migrations;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using SQLitePCL;
using WebAPI.Controllers;
using WebAPI.Dto;

namespace IntegrationTest;

public class QuizApiGetRequestTest : IClassFixture<QuizAppTestFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly QuizAppTestFactory<Program> _app;
    private readonly QuizDbContext _context;
    public QuizApiGetRequestTest(QuizAppTestFactory<Program> app)
    {
        _app = app;
        _client = app.CreateClient();
        using (var scope = app.Services.CreateScope())
        {
            _context = scope.ServiceProvider.GetService<QuizDbContext>();
            var items = new HashSet<QuizItemEntity>
            {
                new()
                {
                    Id = 1, CorrectAnswer = "7", Question = "2 + 5", IncorrectAnswers =
                        new HashSet<QuizItemAnswerEntity>
                        {
                            new() {Id = 11, Answer = "5"},
                            new() {Id = 12, Answer = "6"},
                            new() {Id = 13, Answer = "8"},
                        }
                },
                new()
                {
                    Id = 2, CorrectAnswer = "8", Question = "2 + 4", IncorrectAnswers =
                        new HashSet<QuizItemAnswerEntity>
                        {
                            new() {Id = 21, Answer = "5"},
                            new() {Id = 22, Answer = "6"},
                            new() {Id = 23, Answer = "7"},
                        }
                },
                new()
                {
                    Id = 3, CorrectAnswer = "3", Question = "9 / 3", IncorrectAnswers =
                        new HashSet<QuizItemAnswerEntity>
                        {
                            new() {Id = 31, Answer = "1"},
                            new() {Id = 32, Answer = "2"},
                            new() {Id = 33, Answer = "4"},
                        }
                }
            };
            if (_context.Quizzes.Count() == 0)
            {
                _context.Quizzes.Add(
                    new QuizEntity
                    {
                        Id = 1,
                        Items = items,
                        Title = "Matematyka"
                    }
                );
                _context.SaveChanges();
            }
        }
    }

    [Fact]
    public async void GetAllShouldReturnOneQuiz()
    {
        //Arrange

        //Act
        var result = await _client.GetFromJsonAsync<List<QuizDto>>("/api/v1/quizzes");

        //Assert
        if (result != null)
        {
            Assert.Single(result);
            Assert.Equal("Matematyka", result[0].Title);
        }
    }

    [Fact]
    public async void GetShouldReturnOkStatus()
    {
        //Arrange

        //Act
        var result = await _client.GetAsync("/api/v1/quizzes");

        //Assert
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Contains("application/json", result.Content.Headers.GetValues("Content-Type").First());
    }

    //TODO dokończyć (pobranie tokena) i poprawić
    [Fact]
    public async void PostShouldReturnResponse()
    {
        var body = new QuizItemAnswerDto() {UserAnswer = "5", UserId = 1};
        
        HttpRequestMessage request = new HttpRequestMessage()
        {
            RequestUri = new Uri("https://localhost:7077/api/v1/quizzes/1/items/1/answers"),
            Method = HttpMethod.Post,
            Headers =
            {
                {HttpRequestHeader.Authorization.ToString(), "Bearer 3789..."}
            },
            Content = JsonContent.Create(body),
        };
        
        var response = await _client.SendAsync(request);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        request =  request = new HttpRequestMessage()
        {
            RequestUri = new Uri("https://localhost:7077/api/v1/quizzes/1/items/1/answers"),
            Method = HttpMethod.Post,
            Headers =
            {
                {HttpRequestHeader.Authorization.ToString(), "Bearer 3789..."}
            },
            Content = JsonContent.Create(body),
        };
        response = await _client.SendAsync(request);
        Assert.Equal( HttpStatusCode.BadRequest, response.StatusCode);
    }
}