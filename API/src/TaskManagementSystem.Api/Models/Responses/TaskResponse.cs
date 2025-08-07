using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Api.Models.Responses;

public record TaskResponse(Guid Id, string Title, Priority Priority, DateTime DueDate, Status Status, string? Description);