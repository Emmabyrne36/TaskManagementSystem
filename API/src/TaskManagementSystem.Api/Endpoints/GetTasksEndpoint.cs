using TaskManagementSystem.Api.Mappers;
using TaskManagementSystem.Api.Models.Requests;
using TaskManagementSystem.Api.Models.Responses;
using TaskManagementSystem.Data.Repositories;

namespace TaskManagementSystem.Api.Endpoints;

public static class GetTasksEndPoint
{
    internal const string Name = "GetAllTasks";

    public static IEndpointRouteBuilder MapGetAllTasks(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndpoints.GetAllTasks, async ([AsParameters] PagedRequest request,  ITaskRepository taskRepository, CancellationToken cancellationToken) =>
        {
            var page = request.Page.GetValueOrDefault(PagedRequest.DefaultPage);
            var pageSize = request.PageSize.GetValueOrDefault(PagedRequest.DefaultPageSize);

            var tasks = await taskRepository.GetAll(page, pageSize, cancellationToken);
            var totalCount = await taskRepository.GetCount(cancellationToken);

            var tasksResponse = tasks.MapToResponse(page, pageSize, totalCount);

            return TypedResults.Ok(tasksResponse);
        })
        .WithName(Name)
        .Produces<TasksResponse>(StatusCodes.Status200OK);

        return app;
    }
}