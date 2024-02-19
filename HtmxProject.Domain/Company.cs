namespace HtmxProject.Domain;

public class Company : Entity<Guid>
{
    public string Name { get; set; } = "";
    public string CompanyNumber { get; set; } = "";

    public bool IsVerified { get; set; }
}

