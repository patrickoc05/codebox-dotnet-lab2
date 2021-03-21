using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.OpenApi.Models;

namespace DotNetWebApi
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
            services.AddCors(options => options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DotNetWebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DotNetWebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();
            //app.UseAuthorization();

            app.UseEndpoints(e =>
            {
                e.MapGet("/", c => c.Response.WriteAsync("Hello world!"));
                
                e.MapGet("hello", context =>
                    context.Response.WriteAsJsonAsync(new { Message = "Hello, visitor"})
                );
                
                e.MapGet("hello/{name}", context => {

                    context.Response.WriteAsync($"Hello, {context.Request.RouteValues["name"]}");

                    return System.Threading.Tasks.Task.CompletedTask;
                });

                e.MapPost("hello", async context => {

                    var value = new StringValues(context.Request.Host.Host);

                    context.Response.Headers.Add("Host-name", value);

                    using var reader = new StreamReader(context.Request.Body, System.Text.Encoding.UTF8);
                    var content = await reader.ReadToEndAsync();

                    System.Console.WriteLine(content);

                    context.Response.StatusCode = 200;
                });

                e.MapControllers();
            });
        }
    }
}
