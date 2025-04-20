using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Blog.Application;

public class AI
{
    private readonly IConfiguration _configuration;
    public AI(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> ChatAsync(string message)
    {
        var url = _configuration["AI:Url"];
        var apiKey = _configuration["AI:ApiKey"];
        //string url = "https://api.aimlapi.com/v1/chat/completions";
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        var requestBody = new
        {
            model = "gpt-3.5-turbo",
            messages = new[]
            {
                new { role = "user", content = message }
            }
        };

        var jsonContent = JsonSerializer.Serialize(requestBody);
        var requestContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(url, requestContent);
        var responseContent = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(responseContent);
        string contentValue = doc?.RootElement
                                 .GetProperty("choices")[0]
                                 .GetProperty("message")
                                 .GetProperty("content")
                                 .GetString();
        return contentValue;
    }
}
