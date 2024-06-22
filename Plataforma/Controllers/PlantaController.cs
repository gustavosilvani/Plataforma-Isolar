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
        private readonly IAlertaService _alertaService;

        public PlantaController(IPlantaService plantaService, INotificacaoHandler notificacaoHandler, IPlantaProducaoService plantaProducaoService, IAlertaService alertaService) : base(notificacaoHandler)
        {
            _plantaService = plantaService;
            _plantaProducaoService = plantaProducaoService;
            _alertaService = alertaService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            return PResult(await _plantaService.ObterTodos());
        }

        [HttpGet("Producao")]
        public async Task<IActionResult> ObterProducaoComPlanta()
        {
            return PResult(await _plantaService.ObterTodosComProducao());
        }

        [HttpGet("Producao/{idPlanta}")]
        public async Task<IActionResult> ObterProducaoPorPlanta(string idPlanta)
        {
            return PResult(await _plantaProducaoService.ObterPorPlanta(idPlanta));
        }

        [HttpGet("Alerta")]
        public async Task<IActionResult> ObterTodosAlertas()
        {
            return PResult(await _alertaService.ObterTodos());
        }
    }
}
