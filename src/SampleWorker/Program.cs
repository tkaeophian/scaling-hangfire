using FireTransit;
using Serilog;

var builder = Host.CreateApplicationBuilder(args);
builder.Logging.AddSerilog();
// Load Configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

builder.Services.UseFireTransit(builder.Configuration);
// builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();