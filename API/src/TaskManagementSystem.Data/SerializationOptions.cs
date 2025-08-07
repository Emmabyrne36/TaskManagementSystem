using System.Text.Json;

namespace TaskManagementSystem.Data;

internal static class SerializationOptions
{
    internal static JsonSerializerOptions JsonSerializerOptions = new() { WriteIndented = true };
}