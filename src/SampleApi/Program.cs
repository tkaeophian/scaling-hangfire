using Contracts;
using FireTransit;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Logging.AddSerilog();
builder.Services.UseFireTransit(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseHangfireDashboard();

app.MapGet("/publish", async ([FromServices] IEventPublisher eventPublisher) =>
    {
        eventPublisher.Enqueue(new SampleMessage
        {
            Content = "Enqueue Message"
        });
        
        eventPublisher.Schedule(new SampleMessage
        {
            Content = "Scheduled Message"
        }, TimeSpan.FromMinutes(1));
        
        var res = await eventPublisher.Request<SampleMessage, SampleResponse>(new SampleMessage
        {
            Content = "Publish Message"
        });
        return Results.Ok($"Message Publish with Response : {res.Content}");
    })
    .WithName("PublishMessage");

// Schedule Recurring Message
await using var scope = app.Services.CreateAsyncScope();
var eventPublisher = scope.ServiceProvider.GetRequiredService<IEventPublisher>();

eventPublisher.Recurring(new SampleMessage
{
    Content = "Recurring Message"
}, Cron.Minutely());

app.Run();