
using System.Text;
using System.Text.Json;
using Footbally.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Footbally.Infrastructure.Services;

public class OpenAiProvider : IAIProvider
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _model;

    public OpenAiProvider(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["OpenAI:ApiKey"] ?? throw new InvalidOperationException("OpenAI:ApiKey is missing.");
        _model = configuration["OpenAI:Model"] ?? "gpt-4o";
    }

    public async Task<AIProviderResult> CompleteAsync(string systemPrompt, string userPrompt, CancellationToken cancellationToken = default)
    {
        try
        {
            var requestBody = new
            {
                model = _model,
                response_format = new { type = "json_object" },
                messages = new[]
                {
                    new { role = "system", content = systemPrompt },
                    new { role = "user", content = userPrompt }
                },
                temperature = 0.3,
                max_tokens = 2000
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

            var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content, cancellationToken);
            var responseString = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                return new AIProviderResult
                {
                    Success = false,
                    ErrorMessage = $"OpenAI API error: {response.StatusCode} - {responseString}"
                };
            }

            using var doc = JsonDocument.Parse(responseString);
            var root = doc.RootElement;

            var messageContent = root
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString() ?? string.Empty;

            var tokensUsed = root
                .GetProperty("usage")
                .GetProperty("total_tokens")
                .GetInt32();

            return new AIProviderResult
            {
                Content = messageContent,
                TokensUsed = tokensUsed,
                ModelUsed = _model,
                Success = true
            };
        }
        catch (Exception ex)
        {
            return new AIProviderResult
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }
}