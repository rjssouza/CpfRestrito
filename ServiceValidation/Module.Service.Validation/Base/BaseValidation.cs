using AutoMapper;
using Module.Dto.Config;
using Module.Dto.Validation;
using Module.Dto.Validation.Api;
using System;

namespace Module.Service.Validation.Base
{
    /// <summary>
    /// Classe base para serviço de validação de regras de negócio
    /// </summary>
    public abstract class BaseValidation : IDisposable
    {
        protected ValidationSummary _summary;
        private bool disposedValue;

        /// <summary>
        /// Factory para conversão de objetos
        /// </summary>
        public IMapper Mapper { get; set; }

        /// <summary>
        /// Objeto de configurações (registrado no início da aplicação)
        /// </summary>
        public SettingsDto Settings { get; set; }

        public BaseValidation()
        {
            _summary = new ValidationSummary();
        }

        /// <summary>
        /// Dispose pattern
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);

            GC.SuppressFinalize(this);
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
                    _summary = null;
                }

                disposedValue = true;
            }
        }

        /// <summary>
        /// Método para disparar gatilho da validação
        /// </summary>
        protected virtual void OnValidated()
        {
            if (_summary.ContainsErrors)
            {
                throw new ValidationException(_summary);
            }
        }

        protected void AddError(string subject, string message)
        {
            _summary.AddError(subject, message);
        }
    }
}