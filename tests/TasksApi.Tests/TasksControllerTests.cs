
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


    }
}

