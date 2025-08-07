using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Api.Models.Requests;

public record CreateTaskRequest(string Title, Priority Priority, DateTime DueDate, Status Status, string? Description = null);