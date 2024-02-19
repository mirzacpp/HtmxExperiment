namespace HtmxProject.Application.Items
{
	public interface IItemsService
	{
		Task<PagedResultDto<ItemsDto>> GetAsync(int page, int pageSize, string searchTerm = null);
	}

	public class PagedResultDto<T> : ListResultDto<T>
	{
		public long TotalCount { get; set; }

		public PagedResultDto(long totalCount, IReadOnlyList<T> items)
		: base(items)
		{
			TotalCount = totalCount;
		}
	}

	public class ListResultDto<T>
	{
		public IReadOnlyList<T> Items
		{
			get => _items ??= new List<T>();
			set => _items = value;
		}

		private IReadOnlyList<T>? _items;

		public ListResultDto(IReadOnlyList<T> items)
		{
			Items = items;
		}
	}
}