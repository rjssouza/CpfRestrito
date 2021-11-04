using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Module.Dto;
using Module.Service.Interface;
using System.Collections.Generic;
using WebApi.Base;

namespace WebApi.Controller
{
    /// <summary>
    /// Controller de cep
    /// </summary>
    [Route("api/cep")]
    [ApiController]
    [AllowAnonymous]
    public class CepController : ServiceController
    {
        /// <summary>
        /// Serviço para gestão de CPF
        /// </summary>
        public IRestrictedCpfService RestrictedCpfService { get; set; }

        /// <summary>
        /// Deleta cpf da lista restrita
        /// </summary>
        /// <param name="cpf">Cpf</param>
        /// <returns>Resultado da transação</returns>
        [HttpDelete("{cpf}")]
        [ProducesResponseType(200, Type = typeof(bool))]
        public IActionResult Delete(string cpf)
        {
            RestrictedCpfService.DeleteByCpf(cpf);

            return Ok(true);
        }

        /// <summary>
        /// Obtém pelo cpf
        /// </summary>
        /// <param name="cpf">Cpf</param>
        /// <returns>Objeto cpf</returns>
        [HttpGet("{cpf}")]
        [ProducesResponseType(200, Type = typeof(RestrictedCpfDto))]
        public IActionResult Get(string cpf)
        {
            var restrictedCpfDto = RestrictedCpfService.GetByCpf(cpf);

            return Ok(restrictedCpfDto);
        }

        /// <summary>
        /// Obtém todos os cpfs da lista restrita
        /// </summary>
        /// <returns>Lista objetos do cpf</returns>
        [HttpGet()]
        [ProducesResponseType(200, Type = typeof(List<RestrictedCpfDto>))]
        public IActionResult Get()
        {
            var restrictedCpfListDto = RestrictedCpfService.GetAll();

            return Ok(restrictedCpfListDto);
        }

        /// <summary>
        /// Adiciona cpf a lista restrita
        /// </summary>
        /// <param name="restrictedCpfDto">Objeto de cpf</param>
        /// <returns>Identificador</returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(string))]
        public IActionResult Post(RestrictedCpfDto restrictedCpfDto)
        {
            var id = RestrictedCpfService.Insert(restrictedCpfDto);

            return Ok(id);
        }
    }
}