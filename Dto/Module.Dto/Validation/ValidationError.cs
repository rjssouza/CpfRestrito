using System;
using System.Collections.Generic;
using System.Linq;

namespace Module.Dto.Validation
{
    /// <summary>
    /// Objeto que representa o erro de validação
    /// </summary>
    public class ValidationError
    {
        private readonly List<string> _errors;
        private readonly string _subject;

        /// <summary>
        /// Flag que indica contém erro
        /// </summary>
        public bool ContainsErrors
        {
            get
            {
                return _errors.Count > 0;
            }
        }

        /// <summary>
        /// Lista de erros
        /// </summary>
        public List<string> Errors
        {
            get
            {
                return _errors;
            }
        }

        /// <summary>
        /// Assunto da validação
        /// </summary>
        public string Subject
        {
            get
            {
                return _subject;
            }
        }

        /// <summary>
        /// Construtor do erro de validação
        /// </summary>
        /// <param name="subject">Assunto</param>
        /// <param name="message">Mensagem de erro</param>
        public ValidationError(string subject, string message)
        {
            _errors = new List<string>() { message };
            _subject = subject;
        }

        /// <summary>
        /// Construtor do erro de validação
        /// </summary>
        /// <param name="subject">Assunto</param>
        /// <param name="messages">Lista de mensagens</param>
        public ValidationError(string subject, List<string> messages)
        {
            _errors = messages;
            _subject = subject;
        }

        /// <summary>
        /// Obtém a mensagens de erro
        /// </summary>
        /// <param name="errorIndicator">Caractere que indica a pontuação do erro</param>
        /// <param name="errorSeparator">Separador </param>
        /// <returns>Mensagem formatada</returns>
        public string GetErrorMessage(string errorIndicator = null, string errorSeparator = null)
        {
            if (!ContainsErrors)
            {
                return null;
            }

            string _errorSeparator = errorSeparator ?? Environment.NewLine;

            List<string> errorsList = Errors
                .Select((message) =>
                {
                    return $"{errorIndicator}{message}";
                }).ToList();

            string errorMessage = string.Join(_errorSeparator, errorsList);

            return errorMessage;
        }
    }
}