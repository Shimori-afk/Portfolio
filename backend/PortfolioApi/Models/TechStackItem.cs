namespace PortfolioApi.Models;

public class TechStackItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public int SortOrder { get; set; }
}