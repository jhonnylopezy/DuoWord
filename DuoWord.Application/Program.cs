
using DuoWord.Presentation.Configurations;
using FastEndpoints;
using Serilog;
using Serilog.Extensions.Logging;

var builder = WebApplication.CreateBuilder();

var logger = Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.AddLoggerConfigs();

var appLogger = new SerilogLoggerFactory(logger)
    .CreateLogger<Program>();

builder.Services.AddServiceConfigs(builder.Configuration, appLogger, builder);

builder.Services.AddFastEndpoints();
var endpoint = builder.Build();
endpoint.UseFastEndpoints();
endpoint.Run();