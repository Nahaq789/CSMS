using CSMS.Domain.DomainService;
using CSMS.Domain.Models;
using CSMS.Infrastracture.Repository.Task;
using MediatR;
using Microsoft.Extensions.Logging;


namespace TestCSMS.Service.Task;

public sealed class TestTaskApi : IClassFixture<TestDatabaseFixture>
{
    private readonly HttpClient _httpClient;
    private readonly IMediator _mediatorMock;
    private readonly ITaskRepository<TaskModel> _taskRepository;
    private readonly ILogger<TaskService> _loggerMock;

    public TestTaskApi(HttpClient httpClient, IMediator mediatorMock, ITaskRepository<TaskModel> taskRepository, ILogger<TaskService> loggerMock)
    {
        _httpClient = httpClient;
        _mediatorMock = mediatorMock;
        _taskRepository = taskRepository;
        _loggerMock = loggerMock;
    }

    [Fact]
    public async ValueTask GetAll()
    {
        var response = await _httpClient.GetAsync("api/Task/");

        var s = await response.Content.ReadAsStringAsync();
        response.EnsureSuccessStatusCode();

        Assert.True(response.IsSuccessStatusCode);
    }

    //[Fact]
    //public async Task CreateTask()
    //{
    //    _mediatorMock.Send(Arg)
    //}
}
