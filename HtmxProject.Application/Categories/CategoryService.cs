using HtmxProject.Application.Base;
using HtmxProject.Database;
using HtmxProject.Domain;
using Microsoft.EntityFrameworkCore;

namespace HtmxProject.Application.Categories
{
	public class CategoryService : ICategoryService
	{
		private readonly HtmxDbContext _context;

		public CategoryService(HtmxDbContext context)
		{
			_context = context;
		}

		public async Task<IReadOnlyCollection<NameValueDto<Guid>>> GetAsNameValueAsync()
		{
			return await _context.Set<Category>()
				.Select(c => new NameValueDto<Guid>
				{
					Name = c.Name,
					Value = c.Id
				})
				.ToListAsync();
		}
	}
}