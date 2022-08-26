using MainProject.InMemoryService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject.InMemoryService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMemoryServices(this IServiceCollection services)
        {
            services.AddScoped<IMemoryCachedBooksRepository, MemoryCachedBooksRepository>();
        
        }
    }
}
