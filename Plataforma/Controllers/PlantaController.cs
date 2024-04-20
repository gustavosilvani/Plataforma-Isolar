using Dominio.Interfaces.Services;
using Infra.CrossCutting.Handlers.Notificacoes;
using Microsoft.AspNetCore.Mvc;

namespace Plataforma.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlantaController : AbstractControllerBase
    {
        private readonly IPlantaService _plantaService;
        private readonly IPlantaProducaoService _plantaProducaoService;

        public PlantaController(IPlantaService plantaService, INotificacaoHandler notificacaoHandler, IPlantaProducaoService plantaProducaoService) : base(notificacaoHandler)
        {
            _plantaService = plantaService;
            _plantaProducaoService = plantaProducaoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            return PResult(await _plantaService.ObterTodos());
        }

        [HttpGet("Producao/{idPlanta}")]
        public async Task<IActionResult> ObterProducaoPorPlanta(string idPlanta)
        {
            return PResult(await _plantaProducaoService.ObterPorPlanta(idPlanta));
        }
    }
}
