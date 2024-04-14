namespace Infra.CrossCutting.Handlers.Notificacoes
{
    public class NotificacaoHandler : INotificacaoHandler
    {
        private List<Notificacao> _notifications;

        public NotificacaoHandler()
        {
            _notifications = new List<Notificacao>();
        }

        public List<Notificacao> GetNotifications() => _notifications;

        public void AddNotification(string message) => _notifications.Add(new Notificacao(message));

        public void AddNotification(List<string> messages)
        {
            foreach (var message in messages)
            {
                AddNotification(message);
            }
        }

        public bool HasNotification() => _notifications.Any();

        public void DisposeNotifications() => _notifications = new List<Notificacao>();
    }
}
