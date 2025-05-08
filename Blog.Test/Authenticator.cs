using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace Blog.Test;

public class Authenticator
{
    private static string _username = "pooya";
    private static string _password = "pooya@987";
    private HttpClient _client;
    public Authenticator(BlogWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    public async Task<string> GetTokenAsync()
    {
        var requestContent = new
        {
            Username = _username,
            Password = _password
        };

        var response = await _client.PostAsJsonAsync("/account/login", requestContent);
        var test = await response.Content.ReadAsStringAsync();
        test.Should().Contain("salam");
        //var responseContent = await response.Content.ReadFromJsonAsync<dynamic>();
        return test;
    }
}
