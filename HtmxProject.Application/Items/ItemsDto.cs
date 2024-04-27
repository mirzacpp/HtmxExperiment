namespace HtmxProject.Application.Items
{
	public sealed class ItemsDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = "";
		public string? Description { get; set; }
		public DateTime ManufacturedAt { get; set; }
		public Guid CompanyId { get; set; }
		public string CompanyName { get; set; } = "";
		public string Category { get; set; } = "";
	}
}