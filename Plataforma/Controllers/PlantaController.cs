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

        public PlantaController(IPlantaService plantaService, INotificacaoHandler notificacaoHandler) : base(notificacaoHandler)
        {
            _plantaService = plantaService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            return PResult(await _plantaService.ObterTodos());
        }
    }
}
