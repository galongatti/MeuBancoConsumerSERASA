using MeuBancoSerasaConsumer.Configurations;
using MeuBancoSerasaConsumer.Model;
using MeuBancoSerasaConsumer.Service;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace MeuBancoSerasaConsumer;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly string _exchangeName;
    private readonly string _routingKey;
    private readonly string _queueName;
    private readonly string _uri;

    public Worker(ILogger<Worker> logger, IConfiguration configurations)
    {
        _logger = logger;

        _exchangeName = configurations.GetValue<string>("RabbitMQConfig:Exchange");
        _routingKey = configurations.GetValue<string>("RabbitMQConfig:RoutingKey");
        _queueName = configurations.GetValue<string>("RabbitMQConfig:QueueName");
        _uri = configurations.GetValue<string>("RabbitMQConfig:URI");
        Configuration.MyConfiguration = configurations;

    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        var factory = new ConnectionFactory()
        {
            Uri = new Uri(_uri),
            ClientProvidedName = "SERASA Receiver App"
        };

        using IConnection connection = factory.CreateConnection();
        using IModel channel = connection.CreateModel();

        channel.ExchangeDeclare(_exchangeName, ExchangeType.Direct);
        channel.QueueDeclare(_queueName, false, false, false, null);
        channel.QueueBind(_queueName, _exchangeName, _routingKey, null);
        channel.BasicQos(0, 1, false);


        EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

        consumer.Received += (sender, args) =>
        {
            byte[] body = args.Body.ToArray();
            string message = Encoding.UTF8.GetString(body);

            Emprestimo? emprestimo = JsonSerializer.Deserialize<Emprestimo>(message);

            if (emprestimo != null)
            {
                EmprestimoService service = new();
                emprestimo = service.ConsultarSerasa(emprestimo);
                service.AtualarEmprestimo(emprestimo);
            }

            channel.BasicAck(args.DeliveryTag, false);
        };

        string consumerTag = channel.BasicConsume(_queueName, false, consumer);

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }

        channel.BasicCancel(consumerTag);
        channel.Close();
        connection.Close();
    }


}
