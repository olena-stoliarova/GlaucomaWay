using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using System.Net;
using System.Text.Json;
using System.Net.Http;
using GlaucomaWay.Models;
using System.Text;

namespace Tests
{
    public class BasicTests
     : IClassFixture<WebApplicationFactory<GlaucomaWay.Startup>>
    {
        private readonly WebApplicationFactory<GlaucomaWay.Startup> _factory;

        public BasicTests(WebApplicationFactory<GlaucomaWay.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_SwaggerReturnSuccess()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/swagger");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetById_CreatesAndGets()
        {
            var client = _factory.CreateClient();

            using var request = new HttpRequestMessage(HttpMethod.Post, "/Vf14");
            var json = JsonSerializer.Serialize(new Vf14ResultModel
            {
                UserId = 3
            });

            using var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            request.Content = stringContent;

            using var created = await client
                .SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);

            created.EnsureSuccessStatusCode();

            var jsonResult = await created.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<int>(await created.Content.ReadAsStringAsync());

            result.Should().BeGreaterOrEqualTo(1);

            var gotById = await client.GetAsync($"/Vf14/{result}");

            gotById.StatusCode.Should().Be(HttpStatusCode.OK);

            var gotByIdResult = await gotById.Content.ReadAsStringAsync();

            var gotByIdVf14 = JsonSerializer.Deserialize<Vf14ResultModel>(gotByIdResult, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            gotByIdVf14.UserId.Should().Be(3);
        }
    }
}
