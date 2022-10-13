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
        private readonly IPadraoBD _padraoBD;

        public UsuarioController(IPadraoBD padraoBD)
        {
            _padraoBD = padraoBD;
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
        public Usuario Salvar(Usuario usuario)
        {
            return _padraoBD.Salvar(usuario);  
        }
    }
}
