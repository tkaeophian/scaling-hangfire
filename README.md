# 🔥 FireTransit Sample Project

This is a sample project demonstrating the use of FireTransit, a scalable background job processing library that integrates Hangfire, MassTransit, and RabbitMQ for distributed job execution in .NET applications.

## Features

- **Scalability** – Distribute job processing across multiple worker nodes.
- **Decoupling** – Services communicate via messages, reducing direct dependencies.
- **Fault Tolerance** – RabbitMQ ensures reliable job distribution.
- **Easy Integration** – Seamlessly connects Hangfire and MassTransit.
- **Multi-Database Support** – Works with PostgreSQL, SQL Server, and MySQL.

## Installation

Add FireTransit to your .NET project:

```sh
 dotnet add package FireTransit
```

## Quick Start

### 1. Configure FireTransit in `Program.cs`

```csharp
var builder = Host.CreateApplicationBuilder(args);

// Add FireTransit
builder.Services.UseFireTransit(builder.Configuration);

var host = builder.Build();
host.Run();
```

### 2. Consume a Job

```csharp
public class SendEmailConsumer : IConsumer<SendEmailMessage>
{
    public async Task Consume(ConsumeContext<SendEmailMessage> context)
    {
        Console.WriteLine($"Sending email to {context.Message.Email}");
    }
}

public class SendEmailMessage : IBaseMessage
{
    public string Name => "Sample Email Message";
    public string Description => "Triggers sending email messages.";
    public int Email { get; set; }
}
```

### 3. Schedule a recurring job

```csharp
var eventPublisher = provider.GetRequiredService<IEventPublisher>();

eventPublisher.Recurring<SendEmailMessage>(new SendEmailMessage {
  Email = "user@example.com"
}, Cron.Minutely());

```

## Use Cases

- **Email Notifications** – Process email-sending jobs asynchronously without blocking the main application.
- **Order Processing** – Handle background processing of customer orders, reducing API response times.
- **Data Importing** – Queue large data imports without overloading your API.
- **Scheduled Reports** – Generate and distribute reports on a schedule.
- **Event-Driven Workflows** – Trigger automated background tasks based on user actions or system events.

## Scaling Up

- Deploy multiple workers to process jobs in parallel.
- Monitor job execution using **Hangfire Dashboard**.
- Keep services independent for better maintainability.
