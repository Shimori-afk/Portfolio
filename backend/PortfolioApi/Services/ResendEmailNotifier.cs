using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using PortfolioApi.Models;

namespace PortfolioApi.Services;

public class ResendEmailNotifier : IEmailNotifier
{
    private readonly HttpClient _http;
    private readonly ILogger<ResendEmailNotifier> _logger;
    private readonly string? _apiKey;
    private readonly string _fromEmail;
    private readonly string _toEmail;

    public ResendEmailNotifier(HttpClient http, IConfiguration configuration, ILogger<ResendEmailNotifier> logger)
    {
        _http = http;
        _logger = logger;
        _apiKey = configuration["RESEND_API_KEY"];
        _fromEmail = configuration["RESEND_FROM_EMAIL"] ?? "Portfolio Contact <onboarding@resend.dev>";
        _toEmail = configuration["RESEND_TO_EMAIL"] ?? "maksym.leonovych@gmail.com";
    }

    public async Task NotifyNewContactMessageAsync(ContactMessage message, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(_apiKey))
        {
            _logger.LogWarning("RESEND_API_KEY is not configured — skipping email notification for contact message {Id}.", message.Id);
            return;
        }

        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.resend.com/emails")
        {
            Headers = { Authorization = new AuthenticationHeaderValue("Bearer", _apiKey) },
            Content = JsonContent.Create(new
            {
                from = _fromEmail,
                to = new[] { _toEmail },
                reply_to = message.Email,
                subject = $"New portfolio contact from {message.Name}",
                html = $"""
                    <p><strong>Name:</strong> {WebUtility.HtmlEncode(message.Name)}</p>
                    <p><strong>Email:</strong> {WebUtility.HtmlEncode(message.Email)}</p>
                    <p><strong>Message:</strong></p>
                    <p>{WebUtility.HtmlEncode(message.Message).Replace("\n", "<br>")}</p>
                    """
            })
        };

        var response = await _http.SendAsync(request, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogError(
                "Resend email notification failed for contact message {Id}: {Status} {Body}",
                message.Id, response.StatusCode, body);
        }
    }
}
