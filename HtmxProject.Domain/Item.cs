namespace HtmxProject.Domain;

public class Item : Entity<Guid>
{
	public string Name { get; set; } = "";
	public string? Description { get; set; }
	public DateTime ManufacturedAt { get; set; }

	public DateTime CreatedAtUtc { get; set; }
	public DateTime? UpdatedAtUtc { get; set; }
	public Guid CompanyId { get; set; }
	public Guid ManufacturerId { get; set; }
	public Guid CategoryId { get; set; }
	public double StockQuantity { get; set; }
	public Guid QuantityUnitId { get; set; }
}
