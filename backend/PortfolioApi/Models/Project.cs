namespace PortfolioApi.Models;

public class Project
{
    public int Id { get; set; }
    public string Title {get; set; } = string.Empty;
    public string Description {get; set; } = string.Empty;
    public string Tags {get; set; } = string.Empty;
    public string? Url {get; set; } = string.Empty;
    public int SortOrder {get; set; }

}
