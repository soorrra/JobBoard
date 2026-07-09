using JobBoard.Domain.Enums;

namespace JobBoard.Domain.Entities; 


public class Application
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid VacancyId { get; set; }
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;
    public DateTime CreateedAt { get; set; } = DateTime.UtcNow;

}