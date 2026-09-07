namespace HtmxProject.Domain;

public class Account : Entity<Guid>
{
    public Guid UserId { get; set; }
    public string PhotoName { get; set; } = null!;
}