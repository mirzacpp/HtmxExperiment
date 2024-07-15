using HtmxProject.Application.Base;

namespace HtmxProject.Application.Categories
{
	public interface ICategoryService
	{
		Task<IReadOnlyCollection<NameValueDto<Guid>>> GetAsNameValueAsync(int take = 20, string? searchTerm = null);
	}
}