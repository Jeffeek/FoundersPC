#region Using derectives

using AutoMapper;
using FoundersPC.API.Utils;
using FoundersPC.Core.Hardware_API.Processors;
using FoundersPC.Core.Hardware_API.Producers;
using FoundersPC.Services;
using FoundersPC.Services.Repositories.CPU;
using FoundersPC.Services.Repositories.ProcessorLineup;
using FoundersPC.Services.Repositories.Producer;
using FoundersPC.Services.Repositories.UoW;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

#endregion

namespace FoundersPC.API
{
	public sealed class Startup
	{
		public Startup(IConfiguration configuration) => Configuration = configuration;

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers()
			        .AddNewtonsoftJson(setup => setup.SerializerSettings.ContractResolver =
				                                    new CamelCasePropertyNamesContractResolver());


			var connection = Configuration.GetConnectionString("FoundercPC.connectionString");
			services.AddDbContext<DbContext, FoundersPCDbContext>(
			                                                      options => options.UseSqlServer(connection,
				                                                      builder =>
					                                                      builder
						                                                      .MigrationsAssembly("FoundersPC.Services")));


			services.AddAutoMapper(typeof(MappingStartup));

			services.AddScoped<ICPUsRepository, CPUsRepository>();
			services.AddScoped<IProducersRepository, ProducersRepository>();
			services.AddScoped<IProcessorLineupsRepository, ProcessorLineupsRepository>();

			services.AddScoped<IUnitOfWork, FoundersPCUnitOfWork>();

			services.AddScoped<IProducerService, ProducersService>();
			services.AddScoped<ICPUService, CPUService>();


			services.AddSwaggerGen(options =>
			                       {
				                       options.SwaggerDoc("v1", new OpenApiInfo
				                                                {
					                                                Title = "FoundersPC.API",
					                                                Version = "v1"
				                                                });
			                       });
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "FoundersPC.API v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints => endpoints.MapControllers());
		}
	}
}