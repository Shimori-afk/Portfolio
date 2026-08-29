using System.ComponentModel.DataAnnotations;

namespace PortfolioApi.Models;

public class ContactRequest
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name must be 100 characters or fewer.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "A valid email address is required.")]
    [StringLength(200)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Message is required.")]
    [StringLength(4000, MinimumLength = 5, ErrorMessage = "Message must be between 5 and 4000 characters.")]
    public string Message { get; set; } = string.Empty;
}
