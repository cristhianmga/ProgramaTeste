using AprendendoNovaVersao.Interface;
using AprendendoNovaVersao.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AprendendoNovaVersao.Negocio
{
    public class AutenticacaoNegocio : IAutenticacaoNegocio
    {
        private readonly IPadraoBD _padraoBD;
        private readonly IConfiguration _configuration;
        public AutenticacaoNegocio(IPadraoBD padraoBD, IConfiguration configuration)
        {
            _padraoBD = padraoBD;
            _configuration = configuration; 
        }

        public string CriarTokenAutenticacao(Usuario usuario)
        {
            bool dadosCorretos = false;
            if (!string.IsNullOrEmpty(usuario.NomeUsuario) && !string.IsNullOrEmpty(usuario.Senha))
            {
                dadosCorretos = _padraoBD.ObterTodos<Usuario>().Any(user => user.NomeUsuario.ToLower() == usuario.NomeUsuario.ToLower() && user.Senha == usuario.Senha);
            }
            else
            {
                throw new ArgumentNullException();
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
                return stringToken;
            }

            throw new AccessViolationException();
        }
    }
}
