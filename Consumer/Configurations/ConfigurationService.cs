﻿using Consumer.Job;
using MassTransit;
using Quartz;
using System.Reflection;

namespace Consumer.Configurations;

public static class ConfigurationService
{
    public static IServiceCollection QuartzJobServices(this IServiceCollection services, IConfiguration configuration)
    {


        var connectionString = configuration.GetConnectionString("quartz");

        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();

            // Configurações do Scheduler
            q.SchedulerName = "MassTransit-Scheduler";
            q.SchedulerId = "AUTO";

            //Quantidade máxima de tarefas que o Thread Pool do Quartz pode executar simultaneamente.
            q.UseDefaultThreadPool(tp =>
            {
                tp.MaxConcurrency = 10;
            });

            // Configuração de conversão de fuso horário
            q.UseTimeZoneConverter();

            // Configuração de Persistência
            q.UsePersistentStore(s =>
            {
                s.UseProperties = true;
                s.RetryInterval = TimeSpan.FromSeconds(15);
                s.UseSqlServer(connectionString!);
                s.UseNewtonsoftJsonSerializer();

                s.UseClustering(c =>
                {
                    c.CheckinMisfireThreshold = TimeSpan.FromSeconds(20);
                    c.CheckinInterval = TimeSpan.FromSeconds(10);
                });
            });

            // Configura o job SuperWorker
            var jobKey = new JobKey("SuperWorkerJob");
            q.AddJob<SuperWorker>(opts => opts.WithIdentity(jobKey));

            // Configura o trigger para o job
            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("SuperWorkerTrigger")
                .StartNow() // O job começará imediatamente quando o scheduler for iniciado.
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(10).RepeatForever()));// A cada 10 segundos o job será executado.
        });


        services.AddQuartzHostedService(options =>
        {
            options.StartDelay = TimeSpan.FromSeconds(5);
            options.WaitForJobsToComplete = true;
        });

        services.AddScoped<SuperWorker>();


        return services;
    }

    public static IServiceCollection MassTransitServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqTransportOptions>(configuration.GetSection("RabbitMqTransport"));

        services.AddMassTransit(x =>
        {
            x.AddPublishMessageScheduler();

            x.AddQuartzConsumers();

            x.AddConsumers(Assembly.GetExecutingAssembly());
            //x.AddConsumer<SampleConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.UsePublishMessageScheduler();

                cfg.ConfigureEndpoints(context);
            });
        });


        services.Configure<MassTransitHostOptions>(options =>
        {
            options.WaitUntilStarted = true;
        });

        return services;
    }


    public static IServiceCollection HealthCheckServices(this IServiceCollection services)
    {
        services.AddHealthChecks().AddCheck<SqlServerHealthCheck>("sql");

        return services;
    }
}
