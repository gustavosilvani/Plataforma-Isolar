namespace Dominio.Enumeradores
{
    public enum TipoIntegracao
    {
        Sungrow = 1
    }

    public static class TipoIntegracaoHelper
    {
        public static string ObterDescricao(this TipoIntegracao tipoIntegracao)
        {
            switch (tipoIntegracao)
            {
                case TipoIntegracao.Sungrow: return "Sungrow";
                default: return string.Empty;
            }
        }
    }

}
