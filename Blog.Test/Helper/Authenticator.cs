using System.Net.Http.Json;
using System.Text.Json;

namespace Blog.Test.Helper;

public class Authenticator
{
    private static string _username = "pooya";
    private static string _password = "admin@987";
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
        var responseContent = await response.Content.ReadAsStringAsync();
        var token = JsonDocument.Parse(responseContent).RootElement.GetProperty("token").GetString();
        return token;
    }
}
