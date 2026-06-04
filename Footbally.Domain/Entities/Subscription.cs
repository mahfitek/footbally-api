namespace Footbally.Domain.Entities;

public class Subscription
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string PlanType { get; set; } = "Monthly";
    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    public DateTime EndDate { get; set; }
    public string Status { get; set; } = "Active"; // Active, Expired, Cancelled

    public User User { get; set; } = null!;
}
