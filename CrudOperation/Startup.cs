using CrudOperation.Middleware;
using CrudOperation.Service;
using Microsoft.OpenApi.Models;

namespace CrudOperation
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
            }));
            services.AddControllers();
            services.AddScoped<IUserService, UserService>();
            services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Implement Swagger UI",
                    Description = "A simple example to Implement Swagger UI",
                });
            });

        }


        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Showing API V1");
            });
            app.UseExceptionHandlerMiddleware();
            app.UseCors("ApiCorsPolicy");
            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        }
    }

}
