using System.Text.Json;
using TaskManagementSystem.Domain.Models;

namespace TaskManagementSystem.Data.Repositories;

public class LocalRepository : ITaskRepository
{
    private const string FilePath = "db/tasks.json";
    private readonly List<TaskModel> _tasks;

    public LocalRepository()
    {
        _tasks = LoadFromFile();
    }
    public Task<IEnumerable<TaskModel>> GetAll(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        => Task.FromResult(_tasks
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize));

    public Task<TaskModel> Create(TaskModel task, CancellationToken cancellationToken = default)
    {
        _tasks.Add(task);
        SaveToFile();

        return Task.FromResult(task);
    }

    public Task<TaskModel?> GetById(Guid id, CancellationToken cancellationToken = default) => Task.FromResult(_tasks.FirstOrDefault(t => t.Id == id));

    public Task<bool> Update(TaskModel updatedTask, CancellationToken cancellationToken = default)
    {
        var index = _tasks.FindIndex(t => t.Id == updatedTask.Id);
        if (index == -1) return Task.FromResult(false);

        _tasks[index] = updatedTask;
        SaveToFile();
        return Task.FromResult(true);
    }

    public Task<bool> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        var task = GetById(id).Result;
        if (task == null) return Task.FromResult(false);

        _tasks.Remove(task);
        SaveToFile();
        return Task.FromResult(true);
    }

    public Task<long> GetCount(CancellationToken cancellationToken = default) => Task.FromResult(_tasks.LongCount());

    private static List<TaskModel> LoadFromFile()
    {
        if (!File.Exists(FilePath))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);
            return [];
        }

        var json = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<List<TaskModel>>(json) ?? [];
    }

    private void SaveToFile()
    {
        var json = JsonSerializer.Serialize(_tasks, SerializationOptions.JsonSerializerOptions);
        File.WriteAllText(FilePath, json);
    }
}