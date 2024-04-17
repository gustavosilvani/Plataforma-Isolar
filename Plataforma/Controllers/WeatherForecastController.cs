using Dominio.Interfaces.Services.Integrações.Sungrow;
using Microsoft.AspNetCore.Mvc;

namespace Plataforma.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
      
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ISungrowAutenticacaoService _sungrowAutenticacaoService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ISungrowAutenticacaoService sungrowAutenticacaoService)
        {
            _logger = logger;
            _sungrowAutenticacaoService = sungrowAutenticacaoService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task GetAsync()
        {
           var teste =  await _sungrowAutenticacaoService.Autenticar();
        }
    }
}
