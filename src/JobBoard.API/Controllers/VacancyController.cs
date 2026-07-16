using JobBoard.Domain.Entities;
using JobBoard.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.API.Controllers;

[ApiController]
[Route("api/[controller]")] // маршрут 
public class VacancyController : ControllerBase
{
    //Dependency Injection
    private readonly JobBoardDbContext _context;

    public VacancyController(JobBoardDbContext context)
    {
        _context = context;
    }

    // Create new vacancy
    [HttpPost]
    public async Task<ActionResult<Vacancy>> Create(Vacancy vacancy)
    {
        vacancy.Id = Guid.NewGuid();
        vacancy.CreatedAt = DateTime.UtcNow;

        _context.Vacancies.Add(vacancy); // add to EF Core
        await _context.SaveChangesAsync(); // send SQL to PostgreSQL

        return CreatedAtAction(nameof(GetAll), new { id = vacancy.Id }, vacancy); // 201 Created
    }

    // get all vacancies
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Vacancy>>> GetAll()
    {
        var vacancies = await _context.Vacancies.ToListAsync();

        return Ok(vacancies); // 200 OK + JSON vacancies
    }

    // get vacancy by id
    [HttpGet("{id}")]
    public async Task<ActionResult<Vacancy>> GetById(Guid id)
    {
        var vacancy = await _context.Vacancies.FindAsync(id);

        if (vacancy is null)
        {
            return NotFound(); // 404 Not Found
        }

        return Ok(vacancy);
    }

    // Update vacancy
    // updetedVacancy - data from Swagger UI
    // vacancy - data from database 
    [HttpPut("{id}")]
    public async Task<ActionResult<Vacancy>> Update(Guid id, Vacancy updatedVacancy)
    {
        var vacancy = await _context.Vacancies.FindAsync(id);

        if (vacancy is null)
        {
            return NotFound(); // 404 Not Found
        }

        vacancy.Title = updatedVacancy.Title;
        vacancy.Description = updatedVacancy.Description;
        vacancy.Company = updatedVacancy.Company;
        vacancy.SalaryFrom = updatedVacancy.SalaryFrom;
        vacancy.SalaryTo = updatedVacancy.SalaryTo;

        await _context.SaveChangesAsync(); // send SQL to PostgreSQL

        return Ok(vacancy);
    }

    // Delete vacancy
    [HttpDelete("{id}")]
    public async Task<ActionResult<Vacancy>> Delete (Guid id)
    {
        var vacancy = await _context.Vacancies.FindAsync(id);

        if (vacancy is null)
        {
            return NotFound(); // 404 Not Found
        }

        _context.Vacancies.Remove(vacancy); // remove from EF Core
        await _context.SaveChangesAsync(); // send SQL to PostgreSQL

        return NoContent(); // 204 No Content
    }
    
}

// work with vacansies using HTTP API