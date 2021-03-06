﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Repo.Classes;
using Repo.Interfaces;
using Repo.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace Repo
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
            services.FilterRegisterExtension();

            //services.AddMvc(option =>
            //{
            //    option.Filters.Add(typeof(CustomExceptionFilter));
            //    option.Filters.Add(typeof(UnitOfWorkFilter));
            //    option.Filters.Add(typeof(ResultFilter));
            //});

                //.SetCompatibilityVersion(CompatibilityVersion.Version_2_2)

            // AutoMapper
            services.AddAutoMapper();

            // Database
            services.AddDbContext<RepoContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:RepoDB"]));

            // Repository
            //services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            //services.AddScoped<IOfficeRepository, OfficeRepository>();
            //services.AddScoped<IPersonRepository, PersonRepository>();
            //services.AddScoped<IDeviceRepository, DeviceRepository>();
            //services.AddScoped<IUsageRepository, UsageRepository>();

            // UnitOfWork
            //services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Service Register based on attributes
            services.ServiceRegisterOnCustomAttribute();

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Shayne Boyer",
                        Email = string.Empty,
                        Url = "https://twitter.com/spboyer"
                    },
                    License = new License
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.ConfigureCustomExceptionMiddleware();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

