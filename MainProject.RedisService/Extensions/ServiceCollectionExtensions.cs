using MainProject.RedisService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject.RedisService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRedisServices(this IServiceCollection services)
        {
            services.AddScoped<IRedisCachedBooksRepository, RedisCachedBooksRepository>();
           
        }
    }
}
