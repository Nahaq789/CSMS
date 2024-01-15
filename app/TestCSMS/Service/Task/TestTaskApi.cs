using CSMS.Domain.DomainService;
using CSMS.Domain.DomainService.Interface;
using CSMS.Domain.Models;
using CSMS.UseCase.Commands.TaskCommand;
using Microsoft.Extensions.Logging;


namespace TestCSMS.Service.Task;

public sealed class TestTaskApi : IClassFixture<TestDatabaseFixture>
{
    private DateTime utcDateTime = DateTime.Now.ToUniversalTime();
    private TestDatabaseFixture _databaseFixture = new TestDatabaseFixture();
    private CancellationToken _token = new CancellationToken();

    public TestTaskApi()
    {
    }

    [Fact]
    public async void CreateTask()
    {
        var command = new CreateTaskCommand(
                        Guid.NewGuid(),
                        "taskaddtest",
                        "this is task test",
                        utcDateTime,
                        Guid.Empty,
                        Guid.Empty
            );

        var service = new TaskService(_databaseFixture.CreateContext());

        ILogger<CreateTaskCommandHandler> loggerMock = new Logger<CreateTaskCommandHandler>(new LoggerFactory());
        var handler = new CreateTaskCommandHandler(service, loggerMock);

        var result = await handler.Handle(command, _token);

        Assert.NotNull(result);
        Assert.IsType<TaskModel>(result);
    }

    [Fact]
    public async void DeleteTask()
    {
        var command = new DeleteTaskCommand(new Guid("DDE4BA55-808E-479F-BE8B-72F69913442F"));

        var service = new TaskService(_databaseFixture.CreateContext());
        ILogger<DeleteTaskCommandHandler> loggerMock = new Logger<DeleteTaskCommandHandler>(new LoggerFactory());

        var handler = new DeleteTaskCommandHandler(service, loggerMock);
        var result = await handler.Handle(command, _token);

        Assert.Equal(result, command.TaskId);
        Assert.IsType<Guid>(result);
    }
}
