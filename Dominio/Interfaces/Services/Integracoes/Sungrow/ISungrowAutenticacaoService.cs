namespace Dominio.Interfaces.Services.Integracoes.Sungrow
{
    public interface ISungrowAutenticacaoService
    {
        Task<string> Autenticar();
    }
}
