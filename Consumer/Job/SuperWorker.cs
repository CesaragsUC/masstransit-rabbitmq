namespace Consumer.Job;

using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Shared;
using System;
using System.Threading.Tasks;



/// <summary>
/// Job Utilizando o Quartz NET
/// Job que envia uma mensagem para a fila
/// </summary>

public class SuperWorker : IJob
{
    readonly IServiceScopeFactory _scopeFactory;

    public SuperWorker(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }



    public async Task Execute(IJobExecutionContext context)
    {
        await using var scope = _scopeFactory.CreateAsyncScope();

        var pub01 = scope.ServiceProvider.GetRequiredService<IMessageScheduler>();

        var pub02 = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();


        //publica a msg na fila com 15 segundos de atraso
        await pub01.SchedulePublish(TimeSpan.FromSeconds(15),
             new DemoMessage { Value = "Hello, World", CreatAt = DateTime.Now });

        //envia a msg na fila
        await pub02.Publish(new { Code = Guid.NewGuid(), CreatAt = DateTime.Now });
    }
}