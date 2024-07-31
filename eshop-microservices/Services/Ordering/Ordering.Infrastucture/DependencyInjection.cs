using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastucture
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastuctureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<AppDbContext>(options =>
            //options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }
    }
}
