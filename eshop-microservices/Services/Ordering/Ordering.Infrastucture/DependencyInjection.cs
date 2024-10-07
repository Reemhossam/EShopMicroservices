using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Data;

namespace Ordering.Infrastucture
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastuctureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISaveChangesInterceptor , AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventInterceptor>();
            services.AddDbContext<ApplicationDbContext>((sp,options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
            );

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            return services;
        }
    }
}
