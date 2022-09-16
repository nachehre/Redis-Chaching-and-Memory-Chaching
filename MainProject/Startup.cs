
using MainProject.Infra.Steven_Solution.Services;
using MainProject.Infra.StevenSolution;
using MainProject.InMemoryService.Extensions;
using MainProject.Infra.Steven_Solution;
using MainProject.RedisService.Extensions;
using MainProject.RepositoryService.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace MainProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen();
           
            services.AddBookServices();
            services.AddRedisServices();
            services.AddMemoryServices();
            //services.AddScoped<IRepository>(src =>
            //{
            //    return new MemoryCachedBooksRepository(new  
            //        RedisCachedBooksRepository(new BookRepositoory());
            //});
            //Steven Solution
            services.AddScoped<InMemoryRepository>();
            services.AddScoped<RedisRepository>();
            services.AddScoped<WebApiRepository>();

            services.AddScoped<IRepository>(p =>
            {
                var redis=p.GetRequiredService<RedisRepository>();
                var memory = p.GetRequiredService<InMemoryRepository>();
                var api = p.GetRequiredService<WebApiRepository>();
                return new CachedRepository(memory, redis, api);

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MainProject v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
