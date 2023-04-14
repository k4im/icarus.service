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
                UserName = _config["RabbitMQUser"],
                Password = _config["RabbitMQPwd"],
            };
            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.QueueDeclare("projetos", true, false, false);
                _channel.QueueBind("projetos", "projeto-trigger", "");
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
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                try 
                {
                    
                    byte[] body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    _channel.BasicAck(ea.DeliveryTag, false);
                    Console.WriteLine($" --> Message recive:  {message}");
                }
                catch(Exception)
                {
                    _channel.BasicNack(ea.DeliveryTag, false, true);
                }

            };
            _channel.BasicConsume(queue: "projetos",
                                 autoAck: false,
                     consumer: consumer);

        }

        private void RabbitMQFailed(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine($"--> Não foi possivel se conectar ao Message Bus: {e}");
        }

    }
}