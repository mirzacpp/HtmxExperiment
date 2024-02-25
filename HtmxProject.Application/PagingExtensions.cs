using HtmxProject.Application.Base;
using Microsoft.EntityFrameworkCore;

namespace HtmxProject.Application
{
    public static class PagingExtensions
	{
		public static async Task<PagedResultDto<TDto>> ToPagedListAsync<TDto>(this IQueryable<TDto> query, int page, int pageSize, CancellationToken cancellationToken = default)
		{
			var count = await query.CountAsync(cancellationToken);
			var data = await query
			.Skip((page - 1) * pageSize)
			.Take(pageSize)
			.ToListAsync(cancellationToken);

			return new PagedResultDto<TDto>(count, data);
		}
	}
}