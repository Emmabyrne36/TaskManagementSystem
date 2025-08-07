using TaskManagementSystem.Domain.Enums;
using TaskManagementSystem.Domain.Models;

namespace TaskManagementSystem.Api.Loggers;

public static class HighPriorityLogger
{
    private static readonly string LogFilePath = "logs/high_priority_requests.log";
    private static readonly SemaphoreSlim _writeLock = new SemaphoreSlim(1, 1);

    public static async Task LogCriticalAsync(TaskModel task, string action)
    {
        if (task.Priority != Priority.High) return;

        var logEntry = $"{DateTime.UtcNow:O} [{action}] High-priority task: {task.Title} (ID: {task.Id})";

        Directory.CreateDirectory(Path.GetDirectoryName(LogFilePath)!);

        await _writeLock.WaitAsync();
        try
        {
            await File.AppendAllTextAsync(LogFilePath, logEntry + Environment.NewLine);
        }
        finally
        {
            _writeLock.Release();
        }
    }
}
