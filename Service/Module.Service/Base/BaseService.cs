using AutoMapper;
using Module.Dto.Config;
using Module.Factory.Interface.Conexao;
using Module.Service.Interface.Base;
using System;

namespace Module.Service.Base
{
    /// <summary>
    /// Classe base para serviços
    /// </summary>
    public abstract class BaseService : IBaseService
    {
        private bool disposedValue;

        /// <summary>
        /// Factory para construção de transação
        /// </summary>
        public IDbTransactionFactory DbTransactionFactory { get; set; }

        /// <summary>
        /// Objeto de configurações (registrado no início da aplicação)
        /// </summary>
        public SettingsDto Settings { get; set; }

        /// <summary>
        /// AutoMapper
        /// </summary>
        protected IMapper Mapper { get; set; }

        public BaseService()
        {
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
        /// Método para atualizar transação
        /// </summary>
        protected void Commit()
        {
            DbTransactionFactory.Commit();
        }

        /// <summary>
        /// Dispose pattern
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                disposedValue = true;
            }
        }

        /// <summary>
        /// Método para abrir transação utilizando o TransacaoDbFactory
        /// </summary>
        protected void OpenTransaction()
        {
            DbTransactionFactory.Open();
        }

        /// <summary>
        /// Método para rollback da transação
        /// </summary>
        protected void Rollback()
        {
            DbTransactionFactory.Rollback();
        }
    }
}