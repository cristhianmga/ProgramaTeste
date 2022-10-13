using AprendendoNovaVersao.Interface;
using AprendendoNovaVersao.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AprendendoNovaVersao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : Controller
    {
        private readonly IAutenticacaoNegocio _autenticacao;

        public AutenticacaoController(IAutenticacaoNegocio autenticacao)
        {
            _autenticacao = autenticacao;
        }


        /// <summary>
        /// Gera um token de autenticação para usar nos lançamentos.
        /// </summary>
        /// <remarks>
        /// Exemplo de Request
        /// 
        ///     POST 
        ///     {
        ///         "nomeUsuario": "Nome de usuário"
        ///         "senha": "senha"
        ///     }
        /// </remarks>
        /// <param name="usuario">Objeto usuário</param>
        /// <returns>Retorna um Result</returns>
        /// <remarks>Retorna o status e o token do jwt</remarks>
        [Produces("application/json")]
        [ProducesResponseType(typeof(ActionResult<string>), 200)]
        [ProducesResponseType(typeof(ActionResult<string>), 401)]
        [ProducesResponseType(typeof(ActionResult<string>), 400)]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult<string> CreateToken(Usuario usuario)
        {
            try
            {
                return _autenticacao.CriarTokenAutenticacao(usuario);
            }
            catch (ArgumentNullException)
            {
                return BadRequest("Dados inválidos.");
            }
            catch (AccessViolationException)
            {
                return Unauthorized();
            }
        }
    }
}
