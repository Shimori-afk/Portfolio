using Microsoft.EntityFrameworkCore;
using PortfolioApi.Models;

namespace PortfolioApi.Data;

public class PortfolioDbContext : DbContext
{
    public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options)
        : base(options)
    {
    }

public DbSet<ContactMessage> ContactMessages => Set<ContactMessage>();
public DbSet<Project> Projects => Set<Project>();
public DbSet<TechStackItem> TechStackItems => Set<TechStackItem>();
}