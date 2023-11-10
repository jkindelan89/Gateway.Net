namespace WebApp;

public class Startup
{
    public IConfiguration Configuration { get; }
    public IWebHostEnvironment Env { get; }

    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        Configuration = configuration;
        Env = env;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Controllers
        //
        services.AddControllers();

        // SPA
        //
        // services.AddSpaStaticFiles(configuration => configuration.RootPath = "ClientApp/build");

        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app)
    {
        // Force HTTS
        //
        app.UseHttpsRedirection();

        // Register the Swagger generator and the Swagger UI middlewares
        //
        app.UseSwagger();
        app.UseSwaggerUI();

        // Routing
        //
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();

            endpoints.Map("/api/{**slug}", context =>
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                return Task.CompletedTask;
            });
        });
        
        // app.UseStaticFiles();
        // app.UseSpaStaticFiles();
        // app.UseSpa(spa =>
        // {
        //     spa.Options.SourcePath = "ClientApp";
        //
        //     if (Env.IsDevelopment())
        //     {
        //         spa.UseProxyToSpaDevelopmentServer("http://localhost:3000");
        //     }
        // });
    }
}