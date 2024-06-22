using Dominio.Interfaces.Services.Integracoes.Sungrow;
using Jobs.Interfaces;

namespace Jobs
{    public class TesteJob : ITesteJob
    {
        private readonly ISungrowGerenciamentoPlantasService _sungrowGerenciamentoPlantasService;
        private readonly ISungrowAlarmesFalhasService _sungrowAlarmesFalhasService;

        public TesteJob(ISungrowGerenciamentoPlantasService sungrowGerenciamentoPlantasService, ISungrowAlarmesFalhasService sungrowAlarmesFalhasService)
        {
            _sungrowGerenciamentoPlantasService = sungrowGerenciamentoPlantasService;
            _sungrowAlarmesFalhasService = sungrowAlarmesFalhasService;
        }

        public void Executar()
        {
            Console.WriteLine($"Teste às {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
            _sungrowGerenciamentoPlantasService.ExecutaCaptura();
            _sungrowAlarmesFalhasService.ExecutaCaptura();
        }
    }
}