namespace JobBoard.Domain.Entities;

public class Vacancy
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public decimal SalaryFrom { get; set; }                    // decimal, not double — so we don't get rounding errors
    public decimal SalaryTo { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}