using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using icarus.estoque.Models;
using Newtonsoft.Json;
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
                // Declarando a fila 
                _channel.QueueDeclare(queue: "projetos", 
                    durable: true, 
                    exclusive: false, 
                    autoDelete: false);
                // Definindo o balanceamento da fila para uma mensagem por vez.
                _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
                
                // Linkando a fila ao exchange
                _channel.QueueBind(queue: "projetos", 
                    exchange: "projeto-trigger", "");
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
            var projeto = Consume(_channel);
            Console.WriteLine($"consumer: {projeto.Name}");
        }
        private ProjectDTO Consume(IModel channel) 
        {
            // Definindo um consumidor
            var consumer = new EventingBasicConsumer(channel);

            var projeto = new ProjectDTO();
            
            // Definindo o que o consumidor recebe
            consumer.Received += (model, ea) =>
            {
                try 
                {
                    // transformando o body em um array
                    byte[] body = ea.Body.ToArray();

                    // transformando o body em string
                    var message = Encoding.UTF8.GetString(body);

                    var projeta = JsonConvert.DeserializeObject<ProjectDTO>(message);
                    var projeto = projeta;
                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
                catch(Exception)
                {
                    channel.BasicNack(ea.DeliveryTag, false, true);
                }

            };


            channel.BasicConsume(queue: "projetos",
                                 autoAck: false,
                     consumer: consumer);                     
            
            return projeto;
        }


        // Metodo para fechar a conexão com o broker 
        private void Dispose()
        {
            Console.WriteLine("Message Bus disposed");
            
            // Verifica se a conexão está aberta e fecha
            if(_channel.IsOpen)
            {
                _channel.Close();
            }
        }

        private void RabbitMQFailed(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine($"--> Não foi possivel se conectar ao Message Bus: {e}");
        }

    }
}