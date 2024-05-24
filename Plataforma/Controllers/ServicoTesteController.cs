using Dominio.Interfaces.Services;
using Infra.CrossCutting.Handlers.Notificacoes;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using System.Net;
using System.Text;
using Infra.CrossCutting.Logs;

namespace Plataforma.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServicoTesteController : AbstractControllerBase
    {
        private readonly IPlantaService _plantaService;
        private readonly IPlantaProducaoService _plantaProducaoService;

        public ServicoTesteController(IPlantaService plantaService, INotificacaoHandler notificacaoHandler, IPlantaProducaoService plantaProducaoService) : base(notificacaoHandler)
        {
            _plantaService = plantaService;
            _plantaProducaoService = plantaProducaoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {

            string serverIp = "187.63.223.222"; 
            int port = 62523;                    

            using (Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    clientSocket.Connect(IPAddress.Parse(serverIp), port);
                    Console.WriteLine("Conectado ao servidor!");

                    string request = "GET DATA";  // Substitua pelo comando correto
                    clientSocket.Send(Encoding.ASCII.GetBytes(request));

                    // Receber resposta
                    byte[] buffer = new byte[4096];
                    int bytesReceived = clientSocket.Receive(buffer);
                    string response = Encoding.ASCII.GetString(buffer, 0, bytesReceived);
                    Console.WriteLine("Dados recebidos: " + response);

                }
                catch (SocketException ex)
                {
                    LogService.TratarErro("Erro de rede: " + ex.Message);
                }
                finally
                {
                    if (clientSocket.Connected)
                    {
                        clientSocket.Shutdown(SocketShutdown.Both);
                        clientSocket.Close();
                    }
                }
            }

            return PResult();
        }
       
    }
}
