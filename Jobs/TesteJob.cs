using Jobs.Interfaces;

namespace Jobs
{    public class TesteJob : IJob
    {
        public void Executar()
        {
            Console.WriteLine($"Teste às {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
        }
    }
}