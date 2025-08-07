namespace TaskManagementSystem.Api.Endpoints;

public static class ApiEndpoints
{
    private const string ApiBase = "api/tasks";

    public const string GetAllTasks = ApiBase;
    public const string GetTaskById = $"{ApiBase}/{{id:guid}}";
    public const string CreateTask = ApiBase;
    public const string UpdateTask = ApiBase;
    public const string DeleteTask = ApiBase;
}