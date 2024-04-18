using Dominio.Interfaces.Services.Integracoes.Sungrow;
using Jobs.Interfaces;

namespace Jobs
{    public class TesteJob : ITesteJob
    {
        private readonly ISungrowGerenciamentoPlantasService _sungrowGerenciamentoPlantasService;

        public TesteJob(ISungrowGerenciamentoPlantasService sungrowGerenciamentoPlantasService)
        {
            _sungrowGerenciamentoPlantasService = sungrowGerenciamentoPlantasService;
        }

        public void Executar()
        {
            Console.WriteLine($"Teste às {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
            _sungrowGerenciamentoPlantasService.ExecutaCaptura();
        }
    }
}