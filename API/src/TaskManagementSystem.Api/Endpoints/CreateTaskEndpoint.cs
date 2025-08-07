using TaskManagementSystem.Api.Loggers;
using TaskManagementSystem.Api.Mappers;
using TaskManagementSystem.Api.Models.Requests;
using TaskManagementSystem.Api.Models.Responses;
using TaskManagementSystem.Data.Repositories;

namespace TaskManagementSystem.Api.Endpoints;

public static class CreateTaskEndpoint
{
    internal const string Name = "CreateTask";

    public static IEndpointRouteBuilder MapCreateTask(this IEndpointRouteBuilder app)
    {
        app.MapPost(ApiEndpoints.CreateTask, async (CreateTaskRequest createRequest, ITaskRepository repository, CancellationToken cancellationToken) =>
        {
            var task = createRequest.MapToModel();
            await HighPriorityLogger.LogCriticalAsync(task, "Created");

            var createdTask = await repository.Create(task, cancellationToken);
            
            return TypedResults.CreatedAtRoute(createdTask.MapToResponse(), GetTaskByIdEndpoint.Name, new
            {
                id = createdTask.Id
            });
        })
        .WithName(Name)
        .Accepts<CreateTaskRequest>("application/json")
        .Produces<TaskResponse>(StatusCodes.Status201Created);

        return app;
    }
}

