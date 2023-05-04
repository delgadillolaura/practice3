using Microsoft.OpenApi.Models;
using UPB.CoreLogic.Managers;
using UPB.PracticeNet.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

if(builder.Environment.IsDevelopment())
{
    Log.Logger = new LoggerConfiguration()
        .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}")
        .WriteTo.RollingFile("logs\\log-{Date}.log", outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}")
        .CreateBootstrapLogger();
}
else if (builder.Environment.EnvironmentName == "QA")
{
    Log.Logger = new LoggerConfiguration()
        .WriteTo.RollingFile("logs\\log-{Date}.log", outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}")
        .CreateBootstrapLogger();
}
else
{
    Log.Logger = new LoggerConfiguration()
        .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}")
        .CreateBootstrapLogger();
}

// Add services to the container
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// After create the builder - UseSerilog
builder.Host.UseSerilog();

var configurationBuilder = new ConfigurationBuilder()
        .SetBasePath(builder.Environment.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

IConfiguration Configuration = configurationBuilder.Build();
string siteTitle = Configuration.GetSection("Title").Value;
string locationPath = Configuration.GetSection("Location").Value;

string? folderPath = Path.GetDirectoryName(locationPath);

if (!string.IsNullOrEmpty(folderPath))
{
    Directory.CreateDirectory(folderPath);
}

builder.Services.AddTransient(_ => new PatientManager(locationPath));

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = siteTitle
    });
});

var app = builder.Build();

// Log application environment
Log.Information("Application running in {Environment} environment", builder.Environment.EnvironmentName);

// Configure the HTTP request pipeline
app.UseGlobalExceptionHandler();

if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "QA" || app.Environment.EnvironmentName == "UAT")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();