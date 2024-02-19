using HtmxProject.Application.Items;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class DependencyRegister
	{
		public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
		{
			services.AddScoped<IItemsService, ItemsService>();

			return services;
		}
	}
}