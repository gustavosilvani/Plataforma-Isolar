namespace Dominio.Interfaces.Services.Integrações.Sungrow
{
    public interface ISungrowAutenticacaoService
    {
        Task<string> Autenticar();
    }
}
