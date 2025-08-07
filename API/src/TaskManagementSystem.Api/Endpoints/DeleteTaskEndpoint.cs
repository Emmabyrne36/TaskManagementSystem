using TaskManagementSystem.Data.Repositories;

namespace TaskManagementSystem.Api.Endpoints;

public static class DeleteTaskEndpoint
{
    internal const string Name = "DeleteTask";

    public static IEndpointRouteBuilder MapDeleteTask(this IEndpointRouteBuilder app)
    {
        app.MapDelete(ApiEndpoints.DeleteTask, async (Guid id, ITaskRepository repository, CancellationToken cancellationToken) =>
        {
            var deleted = await repository.Delete(id, cancellationToken);
            return deleted ? Results.NoContent() : Results.NotFound();
        })
        .WithName(Name)
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);

        return app;
    }
}

