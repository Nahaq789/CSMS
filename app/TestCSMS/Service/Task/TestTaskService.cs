﻿using System.IO.Pipelines;
using CSMS.DomainService;
using CSMS.DomainService.Interface;
using CSMS.GlobalEnum;
using CSMS.Models;

namespace TestCSMS.Service.Task;

public class TestTaskService : IClassFixture<TestTaskService>
{
    private ApplicationDbContext _context;

    public TestTaskService(ApplicationDbContext context)
    {
        this._context = context;
    }

    [Fact]
    public async void GetByID()
    {
        var reader = new TaskService(_context);
        TaskModel taskModel = new TaskModel(
            Guid.NewGuid(),
            "tasktest",
            "this is task test",
            DateTime.Now,
            Guid.Empty,
            Guid.Empty
        );
        await reader.Add(taskModel);
        var result = await reader.GetByID(taskModel.TaskId);

        Assert.IsAssignableFrom<ITaskService<TaskModel>>(reader);
        Assert.NotNull(result);
        Assert.Equal(result.TaskId, taskModel.TaskId);
    }

    [Fact]
    public async void GetAll()
    {
        var reader = new TaskService(_context);
        
    }

    [Fact]
    public async void Add()
    {
        var reader = new TaskService(_context);
        TaskModel taskModel = new TaskModel(
            Guid.NewGuid(),
            "taskaddtest",
            "this is task test",
            DateTime.Now,
            Guid.Empty,
            Guid.Empty
        );
        await reader.Add(taskModel);
        var result = await reader.GetByID(taskModel.TaskId);
        Assert.IsAssignableFrom<ITaskService<TaskModel>>(reader);
        Assert.NotNull(result);
        Assert.Equal(result.TaskId, taskModel.TaskId);
    }

    [Fact]
    public async void Update()
    {
        var reader = new TaskService(_context);
        TaskModel beforeTaskModel = new TaskModel(
            Guid.NewGuid(),
            "task before update test",
            "this is before task test",
            DateTime.Now,
            Guid.Empty,
            Guid.Empty
        );
        await reader.Add(beforeTaskModel);
        TaskModel afterTaskModel = new TaskModel(
            Guid.NewGuid(),
            "task after update test",
            "this is after task test",
            DateTime.Now,
            Guid.Empty,
            Guid.Empty
        );
        await reader.Update(afterTaskModel);
        Assert.IsAssignableFrom<ITaskService<TaskModel>>(reader);
        Assert.NotEqual(beforeTaskModel.TaskName, afterTaskModel.TaskName);
        Assert.NotEqual(beforeTaskModel.Contents, afterTaskModel.Contents);
        Assert.Equal(beforeTaskModel.Deadline, afterTaskModel.Deadline);
        Assert.Equal(beforeTaskModel.TaskId, afterTaskModel.TaskId);
    }

    [Fact]
    public async void Delete()
    {
        var reader = new TaskService(_context);
        
        TaskModel taskModel = new TaskModel(
            Guid.NewGuid(),
            "task delete test",
            "this is delete test",
            DateTime.Now,
            Guid.Empty,
            Guid.Empty
        );

        await reader.Add(taskModel);
        var result = await reader.Delete(taskModel);

        Assert.IsAssignableFrom<ITaskService<TaskModel>>(reader);
        await Assert.ThrowsAsync<NullReferenceException>(async () => await reader.GetByID(taskModel.TaskId));
        Assert.Equal(GlobalEnum.DeleteResult.Success, result);
    }
}