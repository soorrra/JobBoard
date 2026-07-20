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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<JobApplication>()
            .HasOne(a => a.User)                       // one application has one user
            .WithMany(u => u.Applications)             // one user has many applications
            .HasForeignKey(a => a.UserId);             // foreign key - UserId

        modelBuilder.Entity<JobApplication>()
            .HasOne(a => a.Vacancy)                    // one application has one vacancy
            .WithMany(v => v.Applications)             // one vacancy has many applications
            .HasForeignKey(a => a.VacancyId);          // foreign key - VacancyId
    }
}                                                      // Referential integrity