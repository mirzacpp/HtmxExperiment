﻿using HtmxProject.Application.Base;
using HtmxProject.Database;
using HtmxProject.Domain;

namespace HtmxProject.Application.Items
{
	public class ItemsService : IItemsService
	{
		private readonly HtmxDbContext _context;

		public ItemsService(HtmxDbContext context)
		{
			_context = context;
		}

		public async Task<PagedResultDto<ItemsDto>> GetAsync(
			int page,
			int pageSize,
			string searchTerm = null,
			Guid? categoryId = null)
		{
			var query = _context.Set<Item>().AsQueryable();

			if (!string.IsNullOrEmpty(searchTerm))
			{
				query = query.Where(c => c.Name.Contains(searchTerm));
			}

			if (categoryId.HasValue)
			{
				query = query.Where(c => c.CategoryId == categoryId.Value);
			}

			return await (from i in query
						  join c in _context.Set<Company>() on i.CompanyId equals c.Id
						  join cat in _context.Set<Category>() on i.CategoryId equals cat.Id
						  orderby i.Id
						  select new ItemsDto
						  {
							  Id = i.Id,
							  Name = i.Name,
							  CompanyId = i.CompanyId,
							  Description = i.Description,
							  ManufacturedAt = i.ManufacturedAt,
							  CompanyName = cat.Name
						  })
			.ToPagedListAsync(page, pageSize, CancellationToken.None) //Get current ctx token source
			.ConfigureAwait(false);
		}
	}
}