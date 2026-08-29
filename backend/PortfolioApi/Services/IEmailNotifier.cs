using PortfolioApi.Models;

namespace PortfolioApi.Services;

public interface IEmailNotifier
{
    Task NotifyNewContactMessageAsync(ContactMessage message, CancellationToken cancellationToken = default);
}
