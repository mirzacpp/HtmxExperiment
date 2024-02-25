namespace HtmxProject.Domain
{
	public class Manufacturer : Entity<Guid>
	{
		public string Name { get; set; }
		public string PictureUrl { get; set; }
	}

	public class Category : Entity<Guid>
	{
		public string Name { get; set; } = "";
		public string? Description { get; set; } = "";
	}
}