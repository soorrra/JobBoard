using JobBoard.Domain.Entities;
using JobBoard.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.API.Controllers;

[ApiController]
[Route("api/[controller]")] // маршрут 
public class ApplicationController : ControllerBase
{
    //Dependency Injection
    private readonly JobBoardDbContext _context;

    public ApplicationController(JobBoardDbContext context)
    {
        _context = context;
    }

    // create new application
    [HttpPost]
    public async Task<ActionResult<JobApplication>> Create(JobApplication application)
    {
        application.Id = Guid.NewGuid();
        application.CreatedAt = DateTime.UtcNow;

        _context.Applications.Add(application); // add to EF Core
        await _context.SaveChangesAsync(); // send SQL to PostgreSQL

        return CreatedAtAction(nameof(GetById), new { id = application.Id }, application); // 201 Created
    }
    
    // get application by id
    [HttpGet("{id}")]
    public async Task<ActionResult<JobApplication>> GetById(Guid id)
    {
        var application = await _context.Applications.FindAsync(id);

        if (application is null)
        {
            return NotFound(); // 404 Not Found
        }

        return Ok(application);
    }

    // get all user applications 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<JobApplication>>> GetAll()
    {
        var applications = await _context.Applications
            .Include(a => a.User)
            .Include(a => a.Vacancy)
            .ToListAsync();

        return Ok(applications); // 200 OK + JSON applications
    }

    // Delete application
    [HttpDelete("{id}")]
    public async Task<ActionResult<JobApplication>> Delete(Guid id)
    {
        var application = await _context.Applications.FindAsync(id);

        if (application is null)
        {
            return NotFound(); // 404 Not Found
        }

        _context.Applications.Remove(application); // remove from EF Core
        await _context.SaveChangesAsync(); // send SQL to PostgreSQL

        return NoContent(); // 204 No Content
    }

}

// work with vacansies using HTTP API