namespace Infra.CrossCutting.Handlers.Notificacoes
{
    public class Notificacao
    {
        public string Message { get; set; }

        public Notificacao(string message)
        {
            Message = message;
        }
    }
}
