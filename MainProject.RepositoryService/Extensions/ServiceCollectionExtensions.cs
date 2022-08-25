using MainProject.RepositoryService.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace MainProject.RepositoryService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBookServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddTransient<IBookRepository, BookRepositoory>();
            //services.AddHttpClient<IBookRepository, BookRepositoory>();
        }
    }
}
