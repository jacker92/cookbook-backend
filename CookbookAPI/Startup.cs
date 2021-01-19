using CookbookAPI.Models;
using CookbookAPI.Repositories;
using CookbookAPI.Services;
using CookbookAPI.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace CookbookAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // requires using Microsoft.Extensions.Options
            services.Configure<MongoDbSettings>(
                Configuration.GetSection(nameof(MongoDbSettings)));

            services.AddSingleton<IMongoDBSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);

            services.AddTransient<IMongoRepository<Recipe>, MongoRepository<Recipe>>();
            services.AddTransient<IMongoRepository<User>, MongoRepository<User>>();

            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<JwtTokenGenerator>();
            services.AddScoped<GoogleTokenValidator>();
            services.AddCors();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
