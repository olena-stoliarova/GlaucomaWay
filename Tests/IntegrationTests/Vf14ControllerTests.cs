using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using System.Net;
using System.Text.Json;
using System.Net.Http;
using GlaucomaWay.Models;
using System.Text;

namespace Tests.IntegrationTests;

public class Vf14ControllerTests
    : IClassFixture<WebApplicationFactory<GlaucomaWay.Startup>>
{
    private readonly WebApplicationFactory<GlaucomaWay.Startup> _factory;

    public Vf14ControllerTests(WebApplicationFactory<GlaucomaWay.Startup> factory)
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

        using var patientRequest = new HttpRequestMessage(HttpMethod.Post, "/Patient");
        var jsonPatient = JsonSerializer.Serialize(new PatientCreateOrUpdateModel
        {
            BithDate = System.DateTime.Today
        });

        using var stringContentPatient = new StringContent(jsonPatient, Encoding.UTF8, "application/json");
        patientRequest.Content = stringContentPatient;

        using var createdPatient = await client
            .SendAsync(patientRequest, HttpCompletionOption.ResponseHeadersRead)
            .ConfigureAwait(false);

        createdPatient.EnsureSuccessStatusCode();

        var resultPatient = JsonSerializer.Deserialize<int>(await createdPatient.Content.ReadAsStringAsync());

        resultPatient.Should().BeGreaterOrEqualTo(1);


        using var VfRequest = new HttpRequestMessage(HttpMethod.Post, "/Vf14");
        var jsonVf14 = JsonSerializer.Serialize(new Vf14CreateOrUpdateModel());


        using var stringContent = new StringContent(jsonVf14, Encoding.UTF8, "application/json");
        VfRequest.Content = stringContent;

        using var created = await client
            .SendAsync(VfRequest, HttpCompletionOption.ResponseHeadersRead)
            .ConfigureAwait(false);

        created.EnsureSuccessStatusCode();

        var result = JsonSerializer.Deserialize<int>(await created.Content.ReadAsStringAsync());

        result.Should().BeGreaterOrEqualTo(1);

        var gotById = await client.GetAsync($"/Vf14/{result}");

        gotById.StatusCode.Should().Be(HttpStatusCode.OK);

        var gotByIdResult = await gotById.Content.ReadAsStringAsync();

        var gotByIdVf14 = JsonSerializer.Deserialize<Vf14ResultModel>(gotByIdResult, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        gotByIdVf14.Patient.Id.Should().Be(resultPatient);
    }
}