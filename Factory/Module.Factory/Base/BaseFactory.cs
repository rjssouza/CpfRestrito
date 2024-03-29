﻿using AutoMapper;
using Module.Dto.Config;
using System;

namespace Module.Factory.Base
{
    /// <summary>
    /// Classe de herança dos factory
    /// </summary>
    public abstract class BaseFactory : IDisposable
    {
        private bool disposedValue;

        /// <summary>
        /// Factory para conversão de objetos automatica utilizando factory
        /// </summary>
        public IMapper ObjectConverterFactory { get; set; }

        /// <summary>
        /// Objeto de configurações (registrado no início da aplicação)
        /// </summary>
        public SettingsDto Settings { get; set; }

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
        /// <param name="disposing"></param>
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
    }
}