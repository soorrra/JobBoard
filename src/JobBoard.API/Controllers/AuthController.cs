using JobBoard.Application.DTOs;
using JobBoard.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobBoard.Domain.Entities;
using JobBoard.Domain.Enums;

namespace JobBoard.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JobBoardDbContext _context;

    public AuthController(JobBoardDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email);

        if (existingUser is not null)
        {
            return BadRequest("User with this email already exists."); // 400
        }

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password); // user password to hash

        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = request.FullName,
            Email = request.Email,
            PasswordHash = passwordHash,
            Role = UserRole.Applicant,
            CreatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email); // get user or null
        
        if (user is null)
        {
            return Unauthorized("Invalid email or password."); // 401 Unauthorized
        }

        var isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

        if (!isPasswordValid)
        {
            return Unauthorized("Invalid email or password."); 
        }

        return Ok(new
        {
            Message = "Login successful."
        });
    }
}