
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace TasksApi.Tests
{
    public class TasksControllerTests :
        IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public TasksControllerTests(
            CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateTask_ReturnsCreated()
        {
            var task = new
            {
                title = "Tarea de prueba automatizada",
                description = "Primera prueba con xUnit",
                isCompleted = false
            };

            var response = await _client.PostAsJsonAsync(
                "/api/Tasks",
                task
            );

            Assert.Equal(
                HttpStatusCode.Created,
                response.StatusCode
            );


        }


        [Fact]
        public async Task CreateTask_WithoutTitle_ReturnsBadRequest()
        {
            var task = new
            {
                title = "",
                description = "Tarea sin título",
                isCompleted = false
            };

            var response = await _client.PostAsJsonAsync(
                "/api/Tasks",
                task
            );

            Assert.Equal(
                HttpStatusCode.BadRequest,
                response.StatusCode
            );
        }


        [Fact]
        public async Task GetTask_NotFound_ReturnsNotFound()
        {
            var response = await _client.GetAsync(
                "/api/Tasks/9999"
            );

            Assert.Equal(
                HttpStatusCode.NotFound,
                response.StatusCode
            );
        }


        [Fact]
        public async Task UpdateAndDeleteTask_ReturnsNoContent()
        {
            var createResponse = await _client.PostAsJsonAsync(
                "/api/Tasks",
                new { title = "Tarea a modificar", isCompleted = false }
            );
            var created = await createResponse.Content
                .ReadFromJsonAsync<TaskResponse>();

            var updateResponse = await _client.PutAsJsonAsync(
                $"/api/Tasks/{created!.Id}",
                new { title = "Tarea modificada", isCompleted = true }
            );
            Assert.Equal(HttpStatusCode.NoContent, updateResponse.StatusCode);

            var updated = await _client
                .GetFromJsonAsync<TaskResponse>($"/api/Tasks/{created.Id}");
            Assert.Equal("Tarea modificada", updated!.Title);
            Assert.True(updated.IsCompleted);

            var deleteResponse = await _client.DeleteAsync(
                $"/api/Tasks/{created.Id}"
            );
            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

            var getResponse = await _client.GetAsync(
                $"/api/Tasks/{created.Id}"
            );
            Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
        }


        private record TaskResponse(int Id, string Title, bool IsCompleted);
    }
}

