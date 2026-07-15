using JobBoard.Domain.Entities;
using JobBoard.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.API.Controllers;

[ApiController]
[Route("api/[controller]")] // маршрут 
public class VacancyController : ControllerBase
{
    private readonly JobBoardDbContext _context;

    //Dependency Injection

    public VacancyController(JobBoardDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Vacancy>>> GetAll()
    {
        var vacancies = await _context.Vacancies.ToListAsync();

        return Ok(vacancies); // 200 OK + JSON vacancies
    }

    [HttpPost]
    public async Task<ActionResult<Vacancy>> Create (Vacancy vacancy)
    {
        vacancy.Id = Guid.NewGuid();
        vacancy.CreatedAt = DateTime.UtcNow;

        _context.Vacancies.Add(vacancy); // add to EF Core
        await _context.SaveChangesAsync(); // send SQL to PostgreSQL

        return CreatedAtAction(nameof(GetAll), new { id = vacancy.Id }, vacancy); // 201 Created
    }
}

// work with vacansies using HTTP API