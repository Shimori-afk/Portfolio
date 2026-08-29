using Microsoft.AspNetCore.Mvc;
using PortfolioApi.Data;
using PortfolioApi.Models;
using Microsoft.EntityFrameworkCore;

namespace PortfolioApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly PortfolioDbContext _db;

    public ProjectsController(PortfolioDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var projects = await _db.Projects
        .OrderBy(p => p.SortOrder)
        .ToListAsync();

        return Ok(projects);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Project>> GetById(int id)
    {
        var project = await _db.Projects.FindAsync(id);

        if (project is null)
        {
            return NotFound();
        }

        return Ok(project);
    }
}