namespace Dominio.Dtos
{
    public class RetornoGenericoDto
    {
        public bool Sucesso { get; set; }
        public object Dados { get; set; }
        public List<string> Mensagens { get; set; }
    }
}
