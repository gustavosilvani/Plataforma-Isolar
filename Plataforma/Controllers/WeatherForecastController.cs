using Dominio.Interfaces.Services.Integracoes.Sungrow;
using Microsoft.AspNetCore.Mvc;

namespace Plataforma.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ISungrowGerenciamentoPlantasService _sungrowGerenciamentoPlantasService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ISungrowGerenciamentoPlantasService sungrowGerenciamentoPlantasService)
        {
            _logger = logger;
            _sungrowGerenciamentoPlantasService = sungrowGerenciamentoPlantasService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task GetAsync()
        {
            _sungrowGerenciamentoPlantasService.ExecutaCaptura();
        }
    }
}
