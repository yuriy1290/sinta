using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog.Web;
using System;

namespace ApiSocialNetwork
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("API �����������");

                var hostBuilder = Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    })
                    .ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                    })
                    .UseNLog();  // ��������� ������������� NLog ��� �����������

                var host = hostBuilder.Build();
                host.Run();
            }
            catch (Exception exception)
            {
                logger.Error(exception, "API ����������� ��-�� ����������");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }
    }
}
