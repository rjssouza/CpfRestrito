using Autofac;
using Autofac.Core;
using Autofac.Extras.Moq;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using Module.Dto.Config;
using Module.Dto.Validation.Api;
using Module.IoC.Interface;
using Module.IoC.Register;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace UnitTest.Base
{
    /// <summary>
    /// Classe base para elaboração de testes unitarios utilizando automock
    /// </summary>
    public abstract class BaseTest : ITestRegister
    {
        /// <summary>
        /// Container autofac
        /// </summary>
        protected IContainer Container;

        /// <summary>
        /// Container automock
        /// </summary>
        protected AutoMock TestMock;

        private bool disposedValue;

        /// <summary>
        /// Objeto de configurações
        /// </summary>
        public HttpContextAccessor HttpContextAccessor => new();

        /// <summary>
        /// Serviço auxiliar para virtualização de objetos utilizados em chamadas e conversão de tipo utilizando automapper
        /// </summary>
        public IMapper Mapper { get; set; }

        /// <summary>
        /// Objeto de configurações (registrado no início da aplicação)
        /// </summary>
        public SettingsDto Settings => new()
        {
            DbConnection = new DbSettingsDto()
            {
                 Default = "\\dados\\sqltest.db"
            },
            ApiServicesUrl = new ExternalApiSettingsDto()
            {
            },
            WebRootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
        };

        public BaseTest()
        {

        }

        [SetUp]
        public void Setup()
        {
            var builder = new ContainerBuilder();

            Register.RegisterDependencyInjection(builder, this);

            MockRepository();
        }

        /// <summary>
        /// Inicia o ciclo de vida autofac para a camada de teste e inicializa o automoq
        /// </summary>
        /// <param name="container">Autofac container</param>
        public void AtribuirCicloVida(IContainer container)
        {
            Container = container;
            //Container.BeginLifetimeScope();
            TestMock = AutoMock.GetLoose();
            Mapper = container.Resolve<IMapper>();
        }

        /// <summary>
        /// Implementação container builder para configuração do ambiente de teste
        /// </summary>
        /// <param name="builder">Container builder autofac</param>
        public void ConfigurarTeste(ContainerBuilder builder)
        {
            builder.RegisterType<HttpContextAccessor>()
                   .AsSelf()
                   .As<IHttpContextAccessor>();

            builder.Register<TesteEnvironment>((context) => new TesteEnvironment()
            {
                ApplicationName = "TesteApp",
                EnvironmentName = "Teste",
                ContentRootPath = $"{Environment.CurrentDirectory}\\Root",
                WebRootPath = $"{Environment.CurrentDirectory}"
            })
            .AsSelf()
            .As<IHostingEnvironment>();
        }

        /// <summary>
        /// Dispose pattern
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Método para ações customizadas ao ativar um tipo já cadastrado na injeção de dependencia (caso necessário)
        /// </summary>
        /// <typeparam name="TypeOf">Tipo</typeparam>
        /// <param name="e">Tipo que está sendo instanciado</param>
        public void OnActivatingInstance<TypeOf>(IActivatingEventArgs<TypeOf> e)
        {
            if (TestMock == null)
                return;

            foreach (TypedService service in e.Component.Services)
            {
                var isRegistered = TestMock.Container
                                                .ComponentRegistry
                                                .Registrations
                                                .Where(t => t.Services.Cast<TypedService>().Any(z => z.ServiceType == service.ServiceType))
                                                .Any();

                if (isRegistered)
                {
                    var newInstance = TestMock.Container.Resolve(service.ServiceType);
                    e.ReplaceInstance(newInstance);
                }
            }
        }

        /// <summary>
        /// Registro de classes de teste caso necessario
        /// </summary>
        /// <param name="builder"></param>
        public void RegisterInternalComponents(ContainerBuilder builder)
        {
        }

        /// <summary>
        /// Testa e analisa se a exceção está em acordo com a mensagem esperada
        /// </summary>
        /// <param name="action">Ação</param>
        /// <param name="expectedMessage">Mensagem esperada</param>
        protected static void AssertValidationExceptionMessage(Action action, string expectedMessage)
        {
            var result = string.Empty;
            try
            {
                action.Invoke();
            }
            catch (ValidationException e)
            {
                result = e.Validation.GetErrorMessage();
            }

            string[] messageList = Regex.Split(result, Environment.NewLine);

            Assert.Contains(expectedMessage, messageList);
        }

        /// <summary>
        /// Dispose pattern
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    TestMock.Dispose();
                    Container.Dispose();
                    Container = null;
                    TestMock = null;
                }

                disposedValue = true;
            }
        }

        protected abstract void MockRepository();
    }

    /// <summary>
    /// Objeto de ambiente herdado para teste
    /// </summary>
    public class TesteEnvironment : IHostingEnvironment
    {
        public string ApplicationName { get; set; }
        public IFileProvider ContentRootFileProvider { get; set; }
        public string ContentRootPath { get; set; }
        public string EnvironmentName { get; set; }
        public IFileProvider WebRootFileProvider { get; set; }
        public string WebRootPath { get; set; }
    }
}