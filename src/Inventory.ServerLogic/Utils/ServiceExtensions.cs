using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ServerLogic.Utils
{
    public static class ServiceExtensions
    {

        public static IServiceCollection AddAllServices(this IServiceCollection services,
                                                        IConfiguration configs)
        {
            services.AddDbContext<AppDbContext>(o => o.UseSqlServer(configs["sqlConnection"]));

            return services;
        }

    }
}
