using TaskManagementSystem.Api.Models.Requests;
using TaskManagementSystem.Api.Models.Responses;
using TaskManagementSystem.Domain.Models;

namespace TaskManagementSystem.Api.Mappers;

public static class TaskMapper
{
    public static TaskResponse MapToResponse(this TaskModel model) 
        => new(model.Id, model.Title, model.Priority, model.DueDate, model.Status, model.Description);

    public static TasksResponse MapToResponse(this IEnumerable<TaskModel> tasks, int page, int pageSize, long totalCount)
    {
        return new TasksResponse()
        {
            Items = tasks.Select(MapToResponse),
            Page = page,
            PageSize = pageSize,
            Total = totalCount
        };
    }

    public static TaskModel MapToModel(this CreateTaskRequest request)
    {
        return new TaskModel(request.Title, request.Priority, request.DueDate, request.Status, request.Description);
    }

    public static TaskModel MapToModel(this UpdateTaskRequest request, Guid id)
    {
        return new TaskModel(id, request.Title, request.Priority, request.DueDate, request.Status, request.Description);
    }
}
