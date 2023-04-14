using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using icarus.projetos.models;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace icarus.projetos.AsyncComm
{
    public class MessageBusService : IMessageBusService
    {
        private readonly IConfiguration _config;
        private IConnection _connection;
        private IModel _channel;

        public MessageBusService(IConfiguration config)
        {
            _config = config;
            var factory = new ConnectionFactory(){
                HostName = _config["RabbitMQ"], 
                UserName = _config["RabbitMQUser"],
                Password = _config["RabbitMQPwd"],
                Port = int.Parse(_config["RabbitPort"])};
            try
            {
                _connection =factory.CreateConnection();
                _channel = _connection.CreateModel();


                _channel.ExchangeDeclare(exchange: "projeto-trigger", type: ExchangeType.Fanout, true, false);
                _channel.QueueDeclare("projetos", true, false, false);
                _channel.QueueBind("projetos", "projeto-trigger", "");
                _connection.ConnectionShutdown += RabbitMQFailed;

                Console.WriteLine("--> Conectado ao Message Bus");
            }
            catch(Exception e)
            {
                Console.WriteLine($"--> Não foi possivel se conectar com o Message Bus: {e.Message}");
            }
        }

        public void publishNewProjeto(ProjectDTO evento)
        {
            var projeto = evento;
            var message = JsonConvert.SerializeObject(projeto.Status);
            if(_connection.IsOpen)
            {
                Console.WriteLine("--> RabbitMQ Connection Open, enviando mensagem...");
                SendMessage(message);
            }

             Console.WriteLine("--> RabbitMQ Connection Closed...");
        }
        private void SendMessage(string evento) 
        {
            var body = Encoding.UTF8.GetBytes(evento);
            _channel.BasicPublish(exchange: "projeto-trigger", 
                routingKey: "", 
                basicProperties: null,
                body: body);
            Console.WriteLine("--> Mensagem enviado");
        }
        private void Dispose()
        {
            Console.WriteLine("Message Bus disposed");
            if(_channel.IsOpen)
            {
                _channel.Close();
            }
        }
        public void RabbitMQFailed(object sender, ShutdownEventArgs e) 
        {
            Console.WriteLine("--> RabbitMQ foi derrubado");
        }
    }
}