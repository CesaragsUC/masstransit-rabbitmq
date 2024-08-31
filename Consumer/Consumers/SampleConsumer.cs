namespace Consumer.Consumers;

using Consumer;
using MassTransit;
using Serilog;
using Shared;
using System.Threading.Tasks;

public class SampleConsumer : IConsumer<DemoMessage>
{

    public SampleConsumer()
    {
       
    }

    public Task Consume(ConsumeContext<DemoMessage> context)
    {
        Log.Information("Received scheduled message: {Value}, Date: {CreatAt}", context.Message.Value, context.Message.CreatAt);

        return Task.CompletedTask;
    }
}