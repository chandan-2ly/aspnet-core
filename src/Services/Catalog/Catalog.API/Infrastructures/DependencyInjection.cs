using Catalog.API.Data;
using Catalog.API.Repositories;
using Catalog.API.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.API.Infrastructures
{
    public static class DependencyInjection
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            #region Services
            services.AddScoped<ICatalogService, CatalogService>();
            #endregion

            #region Repositories
            services.AddScoped<ICatalogContext, CatalogContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            #endregion
        }
    }
}
