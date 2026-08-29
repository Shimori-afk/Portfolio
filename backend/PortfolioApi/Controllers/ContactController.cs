using Microsoft.AspNetCore.Mvc;
using PortfolioApi.Data;
using PortfolioApi.Models;
using PortfolioApi.Services;

namespace PortfolioApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly PortfolioDbContext _db;
    private readonly IEmailNotifier _emailNotifier;
    private readonly ILogger<ContactController> _logger;

    public ContactController(PortfolioDbContext db, IEmailNotifier emailNotifier, ILogger<ContactController> logger)
    {
        _db = db;
        _emailNotifier = emailNotifier;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ContactRequest request)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var entity = new ContactMessage
        {
            Name = request.Name,
            Email = request.Email,
            Message = request.Message
        };

        _db.ContactMessages.Add(entity);
        await _db.SaveChangesAsync();

        _logger.LogInformation("Saved contact message {Id} from {Email}", entity.Id, entity.Email);

        try
        {
            await _emailNotifier.NotifyNewContactMessageAsync(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email notification for contact message {Id}", entity.Id);
        }

        return Ok(new { status = "ok", message = "Thanks — your message was received." });
    }
}