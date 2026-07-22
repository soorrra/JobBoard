using JobBoard.Domain.Entities;
using JobBoard.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.API.Controllers;

[ApiController]
[Route("api/[controller]")] // маршрут 
public class UserController : ControllerBase
{
    //Dependency Injection
    private readonly JobBoardDbContext _context;

    public UserController(JobBoardDbContext context)
    {
        _context = context;
    }

    // create new user
    [HttpPost]
    public async Task<ActionResult<User>> Create(User user)
    {
        user.Id = Guid.NewGuid();
        user.CreatedAt = DateTime.UtcNow;

        _context.Users.Add(user); // add to EF Core
        await _context.SaveChangesAsync(); // send SQL to PostgreSQL

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user); // 201 Created
    }

    // get user by id
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetById(Guid id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user is null)
        {
            return NotFound(); // 404 Not Found
        }

        return Ok(user);
    }

}

// work with users using HTTP API
