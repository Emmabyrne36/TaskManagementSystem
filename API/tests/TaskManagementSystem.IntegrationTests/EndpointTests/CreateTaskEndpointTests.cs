using System.Net.Http.Json;
using Newtonsoft.Json;
using Shouldly;
using TaskManagementSystem.Api.Models.Requests;
using TaskManagementSystem.Api.Models.Responses;
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.IntegrationTests.EndpointTests;

public class CreateTaskEndpointTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task CreateTask_ReturnsCreatedTask_With201Status()
    {
        // Arrange
        var createRequest = new CreateTaskRequest(
            Title: "Integration Test Task",
            Priority: Priority.High,
            DueDate: DateTime.UtcNow.AddDays(7),
            Status: Status.InProgress,
            Description: "Test description"
        );

        // Act
        var response = await _client.PostAsJsonAsync("/api/tasks", createRequest);

        // Assert
        response.StatusCode.ShouldBe(System.Net.HttpStatusCode.Created);

        var responseContent = await response.Content.ReadAsStringAsync();
        var taskResponse = JsonConvert.DeserializeObject<TaskResponse>(responseContent);

        taskResponse.ShouldNotBeNull();
        taskResponse!.Title.ShouldBe(createRequest.Title);
        taskResponse.Priority.ShouldBe(createRequest.Priority);
        taskResponse.Status.ShouldBe(createRequest.Status);
        taskResponse.Description.ShouldBe(createRequest.Description);

        response.Headers.Location.ShouldNotBeNull();
        response.Headers.Location!.ToString().ShouldContain(taskResponse.Id.ToString());
    }
}
