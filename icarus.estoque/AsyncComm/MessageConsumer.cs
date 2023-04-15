using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using icarus.estoque.Data;
using icarus.estoque.Models;
using icarus.estoque.Repository;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace icarus.estoque.AsyncComm
{
    public class MessageConsumer : IMessageConsumer
    {
        private readonly IConfiguration _config;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IRepoEstoque _repo;

        private readonly DataContextEstoque _db;

        public MessageConsumer(IConfiguration config, IRepoEstoque repo, DataContextEstoque db)
        {
            _config = config;
            _repo = repo;
            _db = db;
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

        public int consumeMessage()
        {
            var teste = Consume(_channel);
            return teste;
        }
        private int Consume(IModel channel) 
        {
            if(channel.MessageCount("projetos") == 0) return 0;
            int QuantidadeDeChapa = 0;
            // Definindo um consumidor
            var consumer = new EventingBasicConsumer(channel); 

            // seta o EventSlim
            var msgsRecievedGate = new ManualResetEventSlim(false);
            
            // Definindo o que o consumidor recebe
            consumer.Received +=  (model, ea) =>
            {
                try 
                {
                    // transformando o body em um array
                    byte[] body = ea.Body.ToArray();

                    // transformando o body em string
                    var message = Encoding.UTF8.GetString(body);
                    var projeto = JsonConvert.DeserializeObject<ProjectDTO>(message);
                    // Repassa o valor da mensagem para a var
                    for (int i = 0; i <= channel.MessageCount("projetos"); i++)
                    {
                        QuantidadeDeChapa += projeto.QuantidadeDeChapa;
                    }
                    // seta o valor no EventSlim
                    msgsRecievedGate.Set();
                    Console.WriteLine("--> Messagem tratada");
                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);

                }
                catch(Exception e)
                {
                    // por motivos de debug a não irá dar requeue
                    channel.BasicNack(ea.DeliveryTag,
                    multiple: false, 
                    requeue: true);
                    Console.WriteLine(e);
                }




            };
            // Consome o evento
            channel.BasicConsume(queue: "projetos",
                         autoAck: false,
             consumer: consumer);
            // Espera pelo valor setado
            msgsRecievedGate.Wait();
            // retorna o valor tratado
            Dispose();
            return QuantidadeDeChapa;
            

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