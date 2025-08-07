using TaskManagementSystem.Domain.Models;

namespace TaskManagementSystem.Data.Repositories;

public interface ITaskRepository
{
    Task<IEnumerable<TaskModel>> GetAll(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);
    Task<TaskModel> Create(TaskModel task, CancellationToken cancellationToken = default);
    Task<TaskModel?> GetById(Guid id, CancellationToken cancellationToken = default);
    Task<bool> Update(TaskModel updatedTask, CancellationToken cancellationToken = default);
    Task<bool> Delete(Guid id, CancellationToken cancellationToken = default);
    Task<long> GetCount(CancellationToken cancellationToken = default);
}

