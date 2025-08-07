using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TaskManagementSystem.Data.Configuration;
using TaskManagementSystem.Domain.Models;

namespace TaskManagementSystem.Data.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly IMongoCollection<TaskModel> _tasksCollection;

    public TaskRepository(IOptions<MongoDbSettings> mongoOptions, IMongoClient mongoClient)
    {
        var settings = mongoOptions.Value;
        var database = mongoClient.GetDatabase(settings.DatabaseName);
        _tasksCollection = database.GetCollection<TaskModel>(settings.TasksCollectionName);
    }

    public async Task<IEnumerable<TaskModel>> GetAll(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        return await _tasksCollection
            .Find(_ => true)
            .Skip((pageNumber - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<TaskModel> Create(TaskModel task, CancellationToken cancellationToken = default)
    {
        await _tasksCollection.InsertOneAsync(task, cancellationToken: cancellationToken);
        return task;
    }

    public async Task<TaskModel?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await _tasksCollection
            .Find(t => t.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> Update(TaskModel updatedTask, CancellationToken cancellationToken = default)
    {
        var result = await _tasksCollection.ReplaceOneAsync(
            t => t.Id == updatedTask.Id,
            updatedTask,
            cancellationToken: cancellationToken);

        return result.ModifiedCount > 0;
    }

    public async Task<bool> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _tasksCollection.DeleteOneAsync(
            t => t.Id == id,
            cancellationToken);

        return result.DeletedCount > 0;
    }

    public async Task<long> GetCount(CancellationToken cancellationToken = default)
    {
        return await _tasksCollection.CountDocumentsAsync(_ => true, cancellationToken: cancellationToken);
    }
}
