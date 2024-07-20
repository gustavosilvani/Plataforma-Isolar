using AutoMapper;
using Dominio.Dtos.Integracoes.Sungrow;
using Dominio.Entidades;
using Dominio.Interfaces.Infra.Repository;
using Dominio.Interfaces.Services;
using Dominio.Interfaces.Services.Integracoes.Sungrow;
using Newtonsoft.Json;

namespace Service.Services.Integracoes.Sungrow
{
    public class SungrowAlarmesFalhasService : SungrowServiceBase, ISungrowAlarmesFalhasService
    {
        private readonly IAlertaRepository _alertaRepository;

        public SungrowAlarmesFalhasService(IHttpService httpService, ISungrowAutenticacaoService autenticacaoService, IAlertaRepository alertaRepository, IMapper mapper)
            : base(httpService, autenticacaoService, mapper)
        {
            _alertaRepository = alertaRepository;
        }

        public async Task ExecutaCaptura()
        {
            string url = $"{_configuracaoIntegracao.UrlBase}getFaultAlarmInfo";
            await ExecutaCaptura(await AutenticarEPreparar(),url);
        }

        protected override async Task Processar(string alertaJson)
        {
            try
            {
                var alertaDto = JsonConvert.DeserializeObject<SungrowRetornoAlertaPlantaDto>(alertaJson);
                await _alertaRepository.InserirAtualizar(x => x.Codigo == alertaDto.IdPs, _mapper.Map<Alerta>(alertaDto));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao processar planta: " + ex.Message);
            }
        }

        protected override Dictionary<string, string> ObterParametros(string token, string pagina)
        {
            return new Dictionary<string, string>
            {
                { "token", token },
                { "appkey", _configuracaoIntegracao.AppKey },
                { "lang", "_pt_BR" },
                { "curPage", pagina },
                { "size", "10" }
            };

        }
    }
}