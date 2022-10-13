using AprendendoNovaVersao.Interface;
using AprendendoNovaVersao.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AprendendoNovaVersao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioNegocio _usuarioNegocio;

        public UsuarioController(IUsuarioNegocio usuarioNegocio)
        {
            _usuarioNegocio = usuarioNegocio;
        }

        /// <summary>
        /// Criar um usuário para login
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
        /// <returns>O usuário incluído</returns>
        /// <remarks>Retorna o usuário incluído</remarks>
        [Produces("application/json")]
        [HttpPost]
        [AllowAnonymous]
        public IResult Salvar(Usuario usuario)
        {
            try
            {
                return Results.Ok(_usuarioNegocio.SalvarUsuario(usuario));
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }
    }
}
