using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Domain.Models;

public record TaskModel
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public string Title { get; init; }
    public Priority Priority { get; init; }
    public DateTime DueDate { get; init; }
    public Status Status { get; init; }
    public string? Description { get; init; }

    public TaskModel(string title, Priority priority, DateTime dueDate, Status status, string? description = null)
    {
        Title = title;
        Priority = priority;
        DueDate = dueDate;
        Status = status;
        Description = description;
    }

    [JsonConstructor]
    public TaskModel(Guid id, string title, Priority priority, DateTime dueDate, Status status, string? description = null)
        : this(title, priority, dueDate, status, description)
    {
        Id = id;
    }
}
