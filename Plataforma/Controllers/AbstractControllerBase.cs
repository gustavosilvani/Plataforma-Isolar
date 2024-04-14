using Dominio.Dtos;
using Infra.CrossCutting.Handlers.Notificacoes;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Plataforma.Controllers
{
    //[ServiceFilter(typeof(ResultadoCustomizadoFiltro))]
    public abstract class AbstractControllerBase : ControllerBase
    {
        private readonly INotificacaoHandler _notificacaoHandler;

        public AbstractControllerBase(INotificacaoHandler notificacaoHandler)
        {
            _notificacaoHandler = notificacaoHandler;
        }

        protected ActionResult PResult(object value = null, HttpStatusCode alternativeStatusCode = HttpStatusCode.OK)
        {

            var existeErro = _notificacaoHandler.HasNotification();

            if (value is null && alternativeStatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(new RetornoGenericoDto
                {
                    Sucesso = false,
                    Mensagens = new List<string>() { "Nenhum registro encontrado." }
                });
            }

            var resposta = new RetornoGenericoDto
            {
                Dados = value,
                Mensagens = existeErro ? _notificacaoHandler.GetNotifications().Select(x => x.Message).ToList() : new List<string>(),
                Sucesso = !existeErro
            };


            return new ObjectResult(resposta);
        }
    }
}
