using CSMS.Domain.DomainService;
using CSMS.Domain.DomainService.Interface;
using CSMS.Domain.Models;
using CSMS.Infrastracture.Repository.Task;
using CSMS.UseCase.Commands.TaskCommand;
using CSMS.UseCase.Commands.TaskCommand.Query;
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
                        Guid.Empty,
                        1
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

    [Fact]
    public async void UpdateTask()
    {
        var newCommand = new UpdateTaskCommand(
                        new Guid("DDE4BA55-808E-479F-BE8B-72F69913442F"),
                        "updateTaskaddtest",
                        "this is task update test",
                        utcDateTime,
                        Guid.NewGuid(),
                        Guid.NewGuid(),
                        1
            );
        var service = new TaskService(_databaseFixture.CreateContext());
        ILogger<UpdateTaskCommandHandler> loggerMock = new Logger<UpdateTaskCommandHandler>(new LoggerFactory());

        var handlere = new UpdateTaskCommandHandler(service, loggerMock);
        var result = await handlere.Handle(newCommand, _token);

        Assert.Equal(result.TaskId, newCommand.TaskId);
        Assert.Equal(result.CustomerId, newCommand.CustomerId);
        Assert.Equal(result.ContractId, newCommand.ContractId);
        Assert.IsType<TaskModel>(result);
    }

    [Fact]
    public async void GetAll()
    {
        var _repository = new TaskRepository(_databaseFixture.CreateContext());
        var query = new GetTaskQuery();

        var handler = new GetTaskQueryHandler(_repository);
        var result = await handler.Handle(query, _token);

        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<TaskModel>>(result);
    }

    [Fact]
    public async void GetById()
    {
        var _repository = new TaskRepository(_databaseFixture.CreateContext());
        var query = new GetTaskByIdQuery(new Guid("DDE4BA55-808E-479F-BE8B-72F69913442F"));

        var handler = new GetTaskByIdQueryHandler(_repository);
        var result = await handler.Handle(query, _token);

        Assert.NotNull(result);
        Assert.IsType<TaskModel>(result);
    }
}
