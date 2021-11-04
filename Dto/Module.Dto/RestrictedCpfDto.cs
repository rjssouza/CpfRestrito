using Module.Dto.Base;
using System;

namespace Module.Dto
{
    /// <summary>
    /// Consulta restrição cpf
    /// </summary>
    public class RestrictedCpfDto : BaseDto
    {
        /// <summary>
        /// 
        /// </summary>
        public RestrictedCpfDto()
        {
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Cpf para consulta de restrição
        /// </summary>
        public string Cpf { get; set; }

        /// <summary>
        /// Data de criação UTC
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Nome do dono do cpf
        /// </summary>
        public string Name { get; set; }
    }
}