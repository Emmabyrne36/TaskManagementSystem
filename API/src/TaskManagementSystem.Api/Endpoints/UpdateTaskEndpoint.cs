using TaskManagementSystem.Api.Loggers;
using TaskManagementSystem.Api.Mappers;
using TaskManagementSystem.Api.Models.Requests;
using TaskManagementSystem.Data.Repositories;

namespace TaskManagementSystem.Api.Endpoints;

public static class UpdateTaskEndpoint
{
    internal const string Name = "UpdateTask";

    public static IEndpointRouteBuilder MapUpdateTask(this IEndpointRouteBuilder app)
    {
        app.MapPut(ApiEndpoints.UpdateTask, async (Guid id, UpdateTaskRequest updateRequest, ITaskRepository repository, CancellationToken cancellationToken) =>
        {
            var existing = await repository.GetById(id, cancellationToken);
            if (existing is null)
            {
                return Results.NotFound();
            }

            var updated = updateRequest.MapToModel(id);
            await HighPriorityLogger.LogCriticalAsync(updated, "Updated");
            var success = await repository.Update(updated);

            return TypedResults.Ok(updated);
        })
        .WithName(Name)
        .Accepts<UpdateTaskRequest>("application/json")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
        return app;
    }
}

