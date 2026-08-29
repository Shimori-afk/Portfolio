using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioApi.Data;
using PortfolioApi.Models;

namespace PortfolioApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TechStackController : ControllerBase
{
    private readonly PortfolioDbContext _db;

    public TechStackController(PortfolioDbContext db)
    {
        _db = db;
    }

    // GET /api/techstack
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TechStackItem>>> GetAll()
    {
        var items = await _db.TechStackItems
            .OrderBy(t => t.SortOrder)
            .ToListAsync();

        return Ok(items);
    }

    // GET /api/techstack/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TechStackItem>> GetById(int id)
    {
        var item = await _db.TechStackItems.FindAsync(id);

        if (item is null)
        {
            return NotFound();
        }

        return Ok(item);
    }
}