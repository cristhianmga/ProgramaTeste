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
        private readonly IConfiguration _configuration;
        private readonly IPadraoBD _padraoBD;

        public AutenticacaoController(IConfiguration configuration, IPadraoBD padraoBD)
        {
            _configuration = configuration;
            _padraoBD = padraoBD;
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
        /// <returns>Retorna um IResult</returns>
        /// <remarks>Retorna o status e o token do jwt</remarks>
        [Produces("application/json")]
        [ProducesResponseType(typeof(IResult), 200)]
        [ProducesResponseType(typeof(IResult), 401)]
        [ProducesResponseType(typeof(IResult), 400)]
        [HttpPost]
        [AllowAnonymous]
        public IResult CreateToken(Usuario usuario)
        {
            bool dadosCorretos = false;
            if (!string.IsNullOrEmpty(usuario.NomeUsuario) && !string.IsNullOrEmpty(usuario.Senha))
            {
                dadosCorretos = _padraoBD.ObterTodos<Usuario>().Any(user => user.NomeUsuario.ToLower() == usuario.NomeUsuario.ToLower() && user.Senha == usuario.Senha);
            }
            else
            {
                return Results.BadRequest("Dados Inválidos");
            }

            if (dadosCorretos)
            {
                var issuer = _configuration["Jwt:Issuer"];
                var audience = _configuration["Jwt:Audience"];
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, usuario.NomeUsuario),
                new Claim(JwtRegisteredClaimNames.Email, usuario.NomeUsuario),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
             }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);
                var stringToken = tokenHandler.WriteToken(token);
                return Results.Ok(stringToken);
            }
            return Results.Unauthorized();
        }
    }
}
