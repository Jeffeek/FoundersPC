using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using FoundersPC.Services;
using FoundersPC.Services.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoundersPC.API
{
    public sealed class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
	        services.AddControllers();

	        var connection = Configuration.GetConnectionString("FoundercPC.connectionString");
	        services.AddDbContext<DbContext, FoundersPCDbContext>(
	                                                   options => options.UseSqlServer(connection,
		                                                   b => b.MigrationsAssembly("FoundersPC.Services")));


	        services.AddScoped<ICPURepository, CPURepository>();

	        services.AddSwaggerGen(c =>
	                               {
		                               c.SwaggerDoc("v1", new OpenApiInfo { Title = "FoundersPC.API", Version = "v1" });
	                               });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FoundersPC.API v1"));
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
