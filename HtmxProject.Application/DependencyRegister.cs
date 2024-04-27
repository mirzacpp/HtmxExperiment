using HtmxProject.Application.Categories;
using HtmxProject.Application.Items;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class DependencyRegister
	{
		public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
		{
			services.AddScoped<IItemsService, ItemsService>();
			services.AddScoped<ICategoryService, CategoryService>();

			return services;
		}
	}
}