// See https://aka.ms/new-console-template for more information

using Core.Logging;
using ISun.CommandLine;
using ISun.Service.Options;
using ISun.Service.Worker;
using ISunDatabase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Serilog;
using Weather.BLL;
using Weather.DAL;
using Weather.Infrastructure.BLLs;
using Weather.Infrastructure.DAL;
using Weather.ISun.BLL;
using Weather.ISun.BLL.Options;
using WeatherIntegrationOptions = ISun.Service.Options.WeatherIntegrationOptions;

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.UseOnlyStandardLogging(builder.Configuration);

builder.Services.AddOptions<WeatherIntegrationOptions>().BindConfiguration(nameof(WeatherIntegrationOptions));
builder.Services.AddOptions<WeatherUpdaterOptions>().BindConfiguration(nameof(WeatherUpdaterOptions));

builder.Services.AddDbContext<DbContext, ISunDbContext>((options) =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("ISunConnectionString"));
});

#region Weather Module
builder.Services.AddHttpClient();
builder.Services.AddTransient<IWeatherDAL, WeatherDAL>();
builder.Services.AddTransient<IWeatherUpdaterBLL, WeatherUpdaterBLL>();
builder.Services.AddTransient<IWeatherGetterBLL, ISunWeatherGetterBLL>();
builder.Services.AddTransient<ISunWeatherIntegrationBLL>();
builder.Services.AddTransient<ISunWeatherIntegrationOptions>(
    (services) => services.GetService<IOptions<WeatherIntegrationOptions>>().Value);
builder.Services.AddTransient<CitiesArgumentExtractor>();
builder.Services.AddHostedService<WeatherUpdaterWorker>();
#endregion

try
{
    var host = builder.Build();
    host.Run();
}
catch (Exception ex)
{
    Log.Error(ex, "Unhandled exception");
}

