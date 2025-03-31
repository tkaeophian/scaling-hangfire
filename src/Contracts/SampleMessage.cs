using FireTransit;

namespace Contracts;

public class SampleMessage : IBaseMessage
{
    public string Name => "Sample Message";
    public string Description => "Triggers when the sample message created.";
    public string Content { get; set; } = string.Empty;
}