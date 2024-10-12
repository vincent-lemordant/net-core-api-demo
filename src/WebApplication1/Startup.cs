using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Services;


public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddEndpointsApiExplorer();
        services.AddControllers();
        services.AddSwaggerGen();

        services.AddScoped(typeof(IBaseRepository<Player>), typeof(PlayerRepository));
        services.AddScoped(typeof(PlayerService));
    }

    public void Configure(WebApplication app)
    {
        app.UseSwagger();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwaggerUI();
        }
        else if (app.Environment.IsProduction())
        {
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root (i.e., https://<your-api>.azurewebsites.net/)
            });
        }

        app.UseHttpsRedirection();

        app.MapControllers();
    }
}