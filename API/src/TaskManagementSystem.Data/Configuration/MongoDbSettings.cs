namespace TaskManagementSystem.Data.Configuration;

public class MongoDbSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string TasksCollectionName { get; set; } = "Tasks";
}
