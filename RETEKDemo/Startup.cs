using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RETEKDemo.DataProvider;
using Swashbuckle.AspNetCore.Swagger;
using FluentValidation.AspNetCore;
using System.Reflection;
using System;
using System.IO;

namespace RETEKDemo
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
            services.AddTransient<IMessageDataProvider, MessageDataProvider>();
            services.AddAutoMapper();

            services.AddMvc(opt => opt.Filters.Add(typeof(ValidatorActionFilter)))
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
            

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "RETEK Demo API", Version = "v1" });

                var fileName = this.GetType().GetTypeInfo().Module.Name.Replace(".dll", ".xml").Replace(".exe", ".xml");
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, fileName));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "RETEK Demo swagger");
                });
            }

            app.UseMvc();
        }
    }
}