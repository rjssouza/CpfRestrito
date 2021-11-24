using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace WebApi
{
    /// <summary>
    /// Classe de entrada da aplica��o net core
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Constru��o de host para iis
        /// </summary>
        /// <param name="args">Argumentos externos</param>
        /// <returns>Host builder</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        /// <summary>
        /// M�todo de entrada da aplicação net core
        /// O aspnet core utiliza de constru��o similar ao modelo de aplica��es como console application ou windows services para que possa rodar como servi�o auto gerenciado
        /// </summary>
        /// <param name="args">Argumentos externos</param>
        public static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webHostBuilder =>
                {
                    webHostBuilder.UseContentRoot(Directory.GetCurrentDirectory())
                        .UseIISIntegration()
                        .UseStartup<Startup>();
                })
                .Build();

            host.Run();
        }
    }
}