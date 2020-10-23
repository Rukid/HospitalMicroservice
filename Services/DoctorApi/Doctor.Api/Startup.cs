using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using DoctorApi.Data.Database;
using DoctorApi.Data.Repository;
using DoctorApi.Domain.Entities;
using DoctorApi.Messaging.Send.Options;
using DoctorApi.Messaging.Send.Sender;
using DoctorApi.Models;
using DoctorApi.Service.Command;
using DoctorApi.Service.Query;
using DoctorApi.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DoctorApi
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
            services.AddOptions();

            services.Configure<RabbitMqConfiguration>(Configuration.GetSection("RabbitMq"));

            services.AddDbContext<DoctorContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddAutoMapper(typeof(Startup));

            services.AddMvc().AddFluentValidation();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Doctor Api",
                    Description = "API to create or update doctors",
                    Contact = new OpenApiContact
                    {
                        Name = "Titov Sergey",
                        Email = "sergotitov77@gmail.com"                        
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var actionExecutingContext =
                        actionContext as ActionExecutingContext;

                    if (actionContext.ModelState.ErrorCount > 0
                        && actionExecutingContext?.ActionArguments.Count == actionContext.ActionDescriptor.Parameters.Count)
                    {
                        return new UnprocessableEntityObjectResult(actionContext.ModelState);
                    }

                    return new BadRequestObjectResult(actionContext.ModelState);
                };
            });

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IDoctorRepository, DoctorRepository>();

            services.AddTransient<IValidator<CreateDoctorModel>, CreateDoctorModelValidator>();
            services.AddTransient<IValidator<UpdateDoctorModel>, UpdateDoctorModelValidator>();

            services.AddTransient<IDoctorUpdateSender, DoctorUpdateSender>();

            services.AddTransient<IRequestHandler<CreateDoctorCommand, Doctor>, CreateDoctorCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateDoctorCommand, Doctor>, UpdateDoctorCommandHandler>();
            services.AddTransient<IRequestHandler<GetDoctorByIdQuery, Doctor>, GetDoctorByIdQueryHandler>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Doctor API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
