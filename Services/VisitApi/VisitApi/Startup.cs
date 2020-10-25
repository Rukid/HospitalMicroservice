using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using AutoMapper;
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
using VisitApi.Data.Database;
using VisitApi.Data.Repository;
using VisitApi.Domain;
using VisitApi.Messaging.Receive.Options;
using VisitApi.Messaging.Receive.Receiver;
using VisitApi.Models;
using VisitApi.Service.Command;
using VisitApi.Service.Query;
using VisitApi.Service.Services;
using VisitApi.Validators;

namespace VisitApi
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

            services.AddDbContext<VisitContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddAutoMapper(typeof(Startup));

            services.AddMvc().AddFluentValidation();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Visit Api",
                    Description = "A simple API to work with hospital visits",
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

            services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(IClientUpdateService).Assembly);

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IVisitRepository, VisitRepository>();

            services.AddTransient<IValidator<VisitModel>, VisitModelValidator>();

            services.AddTransient<IRequestHandler<GetInitialVisitsQuery, List<Visit>>, GetInitialVisitsQueryHandler>();
            services.AddTransient<IRequestHandler<GetVisitByIdQuery, Visit>, GetVisitByIdQueryHandler>();
            services.AddTransient<IRequestHandler<GetVisitByClientGuidQuery, List<Visit>>, GetVisitByClientGuidQueryHandler>();
            services.AddTransient<IRequestHandler<CreateVisitCommand, Visit>, CreateVisitCommandHandler>();
            services.AddTransient<IRequestHandler<VisitCompletedCommand, Visit>, VisitCompletedCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateVisitCommand>, UpdateVisitCommandHandler>();
            services.AddTransient<IClientUpdateService, ClientUpdateService>();

            services.AddHostedService<ClientUpdateReceiver>();
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order API V1");
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
