using JobBoard.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Infrastructure.Data;

public class JobBoardDbContext : DbContext
{
    public JobBoardDbContext(DbContextOptions<JobBoardDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Vacancy> Vacancies { get; set; }
    public DbSet<JobApplication> Applications { get; set; }
}