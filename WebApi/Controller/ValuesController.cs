using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Module.Service.Interface;
using WebApi.Base;

namespace WebApi.Controller
{
    /// <summary>
    /// Controller de teste de template 
    /// </summary>
    [Route("api/values")]
    [ApiController]
    [AllowAnonymous]
    public class ValuesController : ServiceController
    {
        public IRestrictedCpfService RestrictedCpfService { get; set; }
        
        public ValuesController(IRestrictedCpfService restrictedCpfService)
        {

        }

        /// <summary>
        /// Obter informação de saúde da api
        /// </summary>
        /// <returns>Mensagem indicando o status da api</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(string))]
        public IActionResult Get()
        {
            return Ok("Estou bem");
        }
    }
}