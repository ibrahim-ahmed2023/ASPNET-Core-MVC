using ContactsManager.Infrastructure.DbContext;
using ContactsManager.Infrastructure.SeedData;
using ContactsManager.Web.Extensions;
using ContactsManager.Web.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Serilog
builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) => {

    loggerConfiguration
    .ReadFrom.Configuration(context.Configuration) //read configuration settings from built-in IConfiguration
    .ReadFrom.Services(services); //read out current app's services and make them available to serilog
});

builder.Services.ConfigureServices(builder.Configuration);


var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try
    {
        DataSeeder.SeedData(dbContext);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error during seeding: {ex.Message}");
        throw;
    }
}

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseHsts(); // Add HSTS for production (Strict Transport Security)
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseExceptionHandlingMiddleware();
}

app.UseSerilogRequestLogging();
app.UseHttpLogging();

app.UseDefaultFiles();

app.UseStaticFiles();
// Enable HTTPS redirection
app.UseHttpsRedirection();

// Add authentication and authorization middleware
app.UseRouting();
app.UseAuthentication(); // Add this
app.UseAuthorization();  // Add this

if (builder.Environment.IsEnvironment("Test") == false)
    Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa");

app.MapControllers();

app.Run();
public partial class Program { } //make the auto-generated Program accessible programmatically