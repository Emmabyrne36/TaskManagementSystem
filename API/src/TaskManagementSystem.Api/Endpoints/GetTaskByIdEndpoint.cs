using TaskManagementSystem.Api.Mappers;
using TaskManagementSystem.Api.Models.Responses;
using TaskManagementSystem.Data.Repositories;

namespace TaskManagementSystem.Api.Endpoints;

public static class GetTaskByIdEndpoint
{
    internal const string Name = "GetTaskById";

    public static IEndpointRouteBuilder MapGetTaskById(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.GetTaskById, async (Guid id, ITaskRepository repository, CancellationToken cancellationToken) =>
        {
            var task = await repository.GetById(id, cancellationToken);
            if (task is null)
            {
                return Results.NotFound();
            }

            var response = task.MapToResponse();

            return TypedResults.Ok(response);
        })
        .WithName(Name)
        .Produces(StatusCodes.Status404NotFound)
        .Produces<TaskResponse>(StatusCodes.Status200OK);

        return app;
    }
}

