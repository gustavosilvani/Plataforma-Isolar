using Serilog;
using System.Reflection;

namespace Infra.CrossCutting.Helpers
{
    public static class LogHelpper
    {
        public static void TratarErro(Exception ex)
        {
            GravarLog(ex.ToString());
        }

        public static void TratarErro(Exception ex, string arquivo)
        {
            GravarLog(ex.ToString(), arquivo);
        }

        public static void TratarErro(string mensagem)
        {
            GravarLog(mensagem);
        }

        public static void TratarErro(string mensagem, string arquivo, bool logErro = true)
        {
            GravarLog(mensagem, arquivo, logErro);
        }

        private static void GravarLog(string mensagem, string prefixo = "", bool logErro = true)
        {
            GravarSerilog(mensagem, prefixo, logErro);
        }

        private static void GravarSerilog(string mensagem, string prefixo, bool logErro)
        {
            try
            {
                DateTime dateTime = DateTime.Now;
                string arquivo = (string.IsNullOrWhiteSpace(prefixo) ? "" : prefixo + "-") + dateTime.Day + "-" + dateTime.Month + "-" + dateTime.Year + ".txt";
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");
                string file = System.IO.Path.Combine(path, arquivo);

                var logger = CreateLogger(file);
                try
                {
                    string timestamp = dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    string logPrefix = string.IsNullOrWhiteSpace(prefixo) ? "" : prefixo + "-";

                    if (logErro)
                        logger.Error("{LogPrefix} {Timestamp} {Message}", logPrefix, timestamp, mensagem);
                    else
                        logger.Information("{LogPrefix} {Timestamp} {Message}", logPrefix, timestamp, mensagem);
                }
                finally
                {
                    (logger as IDisposable)?.Dispose();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }

        private static ILogger CreateLogger(string filePath)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var applicationName = assembly.GetName().Name;

            return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", applicationName)
                .WriteTo.Console()
                .WriteTo.Debug()
                .WriteTo.File(filePath, rollingInterval: RollingInterval.Infinite, shared: true, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}\n")
                .CreateLogger();
        }
    }
}
