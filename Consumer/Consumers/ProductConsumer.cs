using MassTransit;
using Serilog;
using Shared;

namespace Consumer.Consumers;

public class ProductConsumer : IConsumer<ProductMessage>
{

    private readonly ILogger<ProductConsumer> logger;
    public ProductConsumer(ILogger<ProductConsumer> logger)
    {
        this.logger = logger;
    }
    public async Task Consume(ConsumeContext<ProductMessage> context)
    {
        Log.Information($"Nova mensagem recebida :" +
            $" {context.Message.Code} {context.Message.Name}");
    }
}
