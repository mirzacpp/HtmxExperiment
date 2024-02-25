using HtmxProject.Application.Base;

namespace HtmxProject.Application.Items
{
    public interface IItemsService
	{
		Task<PagedResultDto<ItemsDto>> GetAsync(int page, int pageSize, string? searchTerm = null);
	}
}