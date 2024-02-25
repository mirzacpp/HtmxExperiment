namespace HtmxProject.Domain;

public class MeasurementUnit : Entity<Guid>
{
	public string Name { get; set; }
	public string ShortName { get; set; }
}