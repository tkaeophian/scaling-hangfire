using Contracts;
using MassTransit;

namespace SampleWorker.Consumers;

public class SampleMessageConsumer : IConsumer<SampleMessage>
{
    public async Task Consume(ConsumeContext<SampleMessage> context)
    {
        Console.WriteLine("(Processed By Web Worker Consumer) - Sample message received {0}: {1}", context.Message.Content, DateTime.Now);
        
        var response = new SampleResponse("This is your response From Worker");
        
        await context.RespondAsync(response);
    }
}