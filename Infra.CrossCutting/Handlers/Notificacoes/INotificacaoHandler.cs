namespace Infra.CrossCutting.Handlers.Notificacoes
{
    public interface INotificacaoHandler
    {
        List<Notificacao> GetNotifications();
        void AddNotification(string message);
        void AddNotification(List<string> messages);
        bool HasNotification();
        void DisposeNotifications();
    }
}