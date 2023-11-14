using MeuBancoSerasaConsumer;

IHostBuilder builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();
    })
    .ConfigureAppConfiguration((hostContext, configBuilder) =>
    {
            configBuilder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .Build();
    });


IHost host = builder.Build();

await host.RunAsync();
