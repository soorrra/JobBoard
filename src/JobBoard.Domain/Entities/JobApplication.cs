using JobBoard.Domain.Enums;

namespace JobBoard.Domain.Entities; 


public class JobApplication
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid VacancyId { get; set; }
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    // navigation properties

    public User? User { get; set; }
    public Vacancy? Vacancy { get; set; }
}
