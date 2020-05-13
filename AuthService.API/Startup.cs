using AuthService.API.ActionFilters;
using AuthService.API.Application.Models.Members;
using AuthService.API.Application.Services.Interfaces;
using AuthService.API.Application.Validations.Member;
using AuthService.Core.DomainExceptions;
using AuthService.Domain.Events;
using AuthService.Domain.Services;
using AuthService.Domain.Services.Interfaces;
using AuthService.RepoDB.Interfaces;
using AuthService.RepoDB.Repositories;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace AuthService.API
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
            services.AddAutoMapper(Assembly.GetEntryAssembly());

            RepoDb.SqlServerBootstrap.Initialize();
            
            services.AddSingleton(opt =>
            {
                var mediator = BuildMediator();
                return mediator;
            });
            
            services.AddTransient<IMemberService, MemberService>();
            services.AddTransient<IAuthService, Application.Services.AuthService>();

            services.AddMvc().AddFluentValidation(fv =>
            {
                fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            })
            .AddDataAnnotationsLocalization()
            .AddControllersAsServices();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth Service API", Version = "v1" });
            });


            services.AddTransient<IValidator<MemberApiModel>, MemberApiModelValidator>();
            services.AddTransient<IDomainExceptionLocalizer, DomainExceptionLocalizer>();
            services.AddTransient<IDomainLocalizationProvider, AspNetCoreDomainLocalizationProvider>();
            services.AddTransient<DomainExceptionResponsesAttribute>();
            services.AddTransient<DomainExceptionResponsesFilterAttribute>();
           

            services.AddScoped<IMemberRepository, MemberRepository>(options =>
            {
                var configuration = options.GetService<IConfiguration>();
                var loggerFactory = options.GetService<ILoggerFactory>();               
                return new MemberRepository(configuration.GetConnectionString(ApiConstants.ConfigurationKeys.AuthDbConnectionString));
            });           

            
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth Service API V1");
            });
        }

        private static IMediator BuildMediator()
        {
            var services = new ServiceCollection();  

            services.AddMediatR(typeof(MemberEmailUpdatedDomainEvent));
                        
            var provider = services.BuildServiceProvider();

            return provider.GetRequiredService<IMediator>();
        }
    }
}
