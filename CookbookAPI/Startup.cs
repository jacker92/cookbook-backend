using CookbookAPI.Models;
using CookbookAPI.Repositories;
using CookbookAPI.Services;
using CookbookAPI.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace CookbookAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton((System.Func<System.IServiceProvider, IAppSettings>)(sp =>
            sp.GetRequiredService<IOptions<Models.AppSettings>>().Value));

            if (WebHostEnvironment.IsDevelopment())
            {
                services.Configure<Models.AppSettings>(
               Configuration.GetSection(nameof(AppSettings)));
            }

            services.AddTransient<IMongoRepository<Recipe>, MongoRepository<Recipe>>();
            services.AddTransient<IMongoRepository<User>, MongoRepository<User>>();

            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILoginService, LoginService>();

            services.AddScoped<JwtTokenGenerator>();
            services.AddScoped<GoogleTokenValidator>();
            services.AddCors();

            services.AddControllers();
            services.AddHttpContextAccessor();
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
