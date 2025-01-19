using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ServerLogic.Utils
{
    public static class ServiceExtensions
    {

        public static IServiceCollection AddAllServices(this IServiceCollection services)
        {
            return services;
        }

    }
}
