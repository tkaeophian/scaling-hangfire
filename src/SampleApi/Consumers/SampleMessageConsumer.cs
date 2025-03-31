using Contracts;
using MassTransit;

namespace SampleApi.Consumers;

public class SampleMessageConsumer : IConsumer<SampleMessage>
{
    public async Task Consume(ConsumeContext<SampleMessage> context)
    {
        Console.WriteLine("(Processed By API Consumer) - Sample message received {0}: {1}", context.Message.Content, DateTime.Now);
        
        var response = new SampleResponse("This is your response From API Consumer");
        
        await context.RespondAsync(response);
    }
}