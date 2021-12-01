using GlobalProject.Business;
using GlobalProject.Domain.Model;
using GlobalProject.Repository.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GlobalProject.Api
{
    public class Startup
    {
        private static string _configRootPath;
        private readonly IEnumerable<KeyValuePair<string, string>> _configSettings;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _configRootPath = Directory.GetCurrentDirectory();

          
            services.AddRazorPages();
            services.AddControllers();
            services.AddHttpClient();
            services.AddHttpContextAccessor();
            services.AddMvc();
            services.AddAuthentication("ApiAuthentication");
            services.AddSwaggerGen(c =>
            {
                
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
           
            //registering data services
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
            
            
            //registring business services
            services.Configure<AuthenticateDatabaseSettings>(
                Configuration.GetSection(nameof(AuthenticateDatabaseSettings)));

            services.AddSingleton<IAuthenticateDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<AuthenticateDatabaseSettings>>().Value);

            services.AddSingleton<AuthenticateService>();
            services.AddSingleton<IAuthenticateDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<AuthenticateDatabaseSettings>>().Value);
            //ConfigureCheckoutSwaggerServices(services, "Unified Checkout Ft Service Swagger", "https://www.contentdenorm.dellsvc", Convert.ToBoolean(ConfigSettingCollection[AppSettingConstants.APIKeyProtectionEnabled]));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
            app.UseSwagger(options =>
            {
                options.SerializeAsV2 = true;
            });
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("swagger/v1/swagger.json", "My API V2");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
