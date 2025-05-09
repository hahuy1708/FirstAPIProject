using FirstAPIProject.Repository;
using Microsoft.OpenApi.Models; 

namespace FirstAPIProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            
            // Register the EmployeeRepository for Dependency Injection
            builder.Services.AddSingleton<IEmployeeRepository, EmployeeRepository>();

            builder.Services.AddControllers();  // Registers MVC controllers with the dependency injection (DI) container.

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi


            builder.Services.AddEndpointsApiExplorer(); // Enables API endpoint discovery, which is required for tools like Swagger.
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My Custom API",
                    Version = "v1",
                    Description = "A Brief Description of My APIs",
                    TermsOfService = new Uri("https://dotnettutorials.net/privacy-policy/"),
                    Contact = new OpenApiContact
                    {
                        Name = "Support",
                        Email = "support@dotnettutorials.net",
                        Url = new Uri("https://dotnettutorials.net/contact/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use Under XYZ",
                        Url = new Uri("https://dotnettutorials.net/about-us/")
                    }
                });
            });

            //builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
                });
                //app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization(); 


            app.MapControllers();

            app.Run();
        }
    }
}
