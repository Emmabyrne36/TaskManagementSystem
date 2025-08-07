using TaskManagementSystem.Api.Mappers;
using TaskManagementSystem.Api.Models.Requests;
using TaskManagementSystem.Domain.Enums;
using TaskManagementSystem.Domain.Models;
using Shouldly;

namespace TaskManagementSystem.UnitTests.MapperTests;
public class TaskMapperTests
{
    [Fact]
    public void MapToResponse_FromTaskModel_ReturnsMappedTaskResponse()
    {
        // Arrange
        var model = new TaskModel(
            Guid.NewGuid(),
            "Test Task",
            Priority.High,
            DateTime.Today,
            Status.Pending,
            "This is a test task."
        );

        // Act
        var response = model.MapToResponse();

        // Assert
        response.ShouldNotBeNull();
        response.Id.ShouldBe(model.Id);
        response.Title.ShouldBe(model.Title);
        response.Priority.ShouldBe(model.Priority);
        response.DueDate.ShouldBe(model.DueDate);
        response.Status.ShouldBe(model.Status);
        response.Description.ShouldBe(model.Description);
    }

    [Fact]
    public void MapToResponse_FromTaskModelList_ReturnsMappedTasksResponse()
    {
        // Arrange
        var models = new List<TaskModel>
            {
                new TaskModel(Guid.NewGuid(), "Task 1", Priority.Low, DateTime.Today, Status.Completed, "Desc 1"),
                new TaskModel(Guid.NewGuid(), "Task 2", Priority.Medium, DateTime.Today.AddDays(1), Status.Pending, "Desc 2"),
            };

        int page = 1;
        int pageSize = 10;
        long totalCount = 2;

        // Act
        var response = models.MapToResponse(page, pageSize, totalCount);

        // Assert
        response.ShouldNotBeNull();
        response.Items.Count().ShouldBe(2);
        response.Page.ShouldBe(page);
        response.PageSize.ShouldBe(pageSize);
        response.Total.ShouldBe(totalCount);

        response.Items.First().Title.ShouldBe("Task 1");
        response.Items.Last().Title.ShouldBe("Task 2");
    }

    [Fact]
    public void MapToModel_FromCreateRequest_ReturnsMappedTaskModel()
    {
        // Arrange
        var request = new CreateTaskRequest("New Task", Priority.Medium, DateTime.Today, Status.InProgress, "Creating a new task.");

        // Act
        var model = request.MapToModel();

        // Assert
        model.ShouldNotBeNull();
        model.Title.ShouldBe(request.Title);
        model.Priority.ShouldBe(request.Priority);
        model.DueDate.ShouldBe(request.DueDate);
        model.Status.ShouldBe(request.Status);
        model.Description.ShouldBe(request.Description);
    }

    [Fact]
    public void MapToModel_FromUpdateRequest_ReturnsMappedTaskModel()
    {
        // Arrange
        var id = Guid.NewGuid();
        var request = new UpdateTaskRequest("Updated Task", Priority.Medium, DateTime.Today, Status.InProgress, "Creating a new task.");

        // Act
        var model = request.MapToModel(id);

        // Assert
        model.ShouldNotBeNull();
        model.Id.ShouldBe(id);
        model.Title.ShouldBe(request.Title);
        model.Priority.ShouldBe(request.Priority);
        model.DueDate.ShouldBe(request.DueDate);
        model.Status.ShouldBe(request.Status);
        model.Description.ShouldBe(request.Description);
    }
}
