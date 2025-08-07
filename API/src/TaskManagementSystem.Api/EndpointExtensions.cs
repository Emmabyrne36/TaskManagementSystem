using TaskManagementSystem.Api.Endpoints;

namespace TaskManagementSystem.Api;

public static class EndpointExtensions
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapCreateTask();
        app.MapGetAllTasks();
        app.MapGetTaskById();
        app.MapUpdateTask();
        app.MapDeleteTask();
        return app;
    }
}
