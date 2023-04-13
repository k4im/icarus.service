using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace icarus.estoque.AsyncComm
{
    public class MessageConsumer : IMessageConsumer
    {
        private IConfiguration _config;
        private IConnection _connection;
        private IModel _channel;

        public MessageConsumer(IConfiguration config)
        {
            _config = config;
            var factory = new ConnectionFactory(){
                HostName = _config["RabbitMQ"],
                Port = int.Parse(_config["RabbitPort"]),
                UserName = "admin",
                Password = "admin",
            };
            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                var queueName = _channel.QueueDeclare().QueueName;
                _channel.QueueDeclare(queueName, false, false, false);
                _channel.QueueBind(queueName, "projeto-trigger", "");
                _connection.ConnectionShutdown += RabbitMQFailed;


                Console.WriteLine("--> Conectado ao Message Bus");
                Console.WriteLine("--> Conectado ao a queue");
            }
            catch(Exception e)
            {
                Console.WriteLine($"--> Não foi possivel se conectar ao Message Bus: {e.Message}");
            }
        }

        public void consumeMessage()
        {
            var queueName = _channel.QueueDeclare().QueueName;
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                byte[] body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($" --> Message recive:  {message}");
            };
            _channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                     consumer: consumer);

        }

        private void RabbitMQFailed(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine($"--> Não foi possivel se conectar ao Message Bus: {e}");
        }

    }
}