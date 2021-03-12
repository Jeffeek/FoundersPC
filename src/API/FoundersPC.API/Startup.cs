#region Using namespaces

using System;
using FoundersPC.API.Application;
using FoundersPC.API.Infrastructure;
using FoundersPC.API.Services;
using FoundersPC.ApplicationShared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

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
			//services.AddCors(options =>
			//                 {
			//                     options.AddPolicy("WebClientPolicy",
			//                                       builder =>
			//                                       {
			//                                           builder.WithOrigins("http://localhost:9000")
			//                                                  .AllowAnyMethod()
			//                                                  .AllowAnyHeader()
			//                                                  .AllowCredentials();
			//                                       });
			//                 });

			services.AddLogging(config => config.AddSerilog(Log.Logger));

			services.AddControllers();

			//
			services.AddHardwareRepositories();

			//
			services.AddHardwareUnitOfWork();

			//
			services.AddFoundersPCHardwareContext(Configuration);

			//
			services.AddHardwareServices();

			//
			services.AddHardwareApplicationExtensions();

			//
			services.AddValidators();

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
					.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
								  config =>
								  {
									  var key = JwtConfiguration.GetSymmetricSecurityKey();
									  config.BackchannelTimeout = TimeSpan.FromSeconds(20);

									  config.TokenValidationParameters = new TokenValidationParameters
																		 {
																				 ValidateAudience = false,
																				 ValidIssuer = JwtConfiguration.Issuer,
																				 ValidAudience = JwtConfiguration.Audience,
																				 IssuerSigningKey = key
																		 };
								  });

			services.AddAuthorization(config =>
									  {
										  config.AddPolicy("Changeable",
														   builder => builder.RequireAuthenticatedUser()
																			 .RequireRole("Administrator",
																						  "Manager")
																			 .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
																			 .Build());

										  config.AddPolicy("Readable",
														   builder => builder.RequireAuthenticatedUser()
																			 .RequireRole("Administrator",
																						  "Manager",
																						  "DefaultUser")
																			 .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
																			 .Build());
									  });

			services.AddApiVersioning(options =>
									  {
										  options.AssumeDefaultVersionWhenUnspecified = true;
										  options.DefaultApiVersion = new ApiVersion(1, 0);
										  options.ReportApiVersions = true;
									  });

			services.AddSwaggerGen(options => options.SwaggerDoc("v1",
																 new OpenApiInfo
																 {
																		 Title = "FoundersPC.API",
																		 Version = "v1.0"
																 }));
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();

				app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json",
																	"FoundersPC.API v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			//app.UseCors("WebClientPolicy");

			app.UseSerilogRequestLogging();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints => endpoints.MapControllers());
		}
	}
}