using AprendendoNovaVersao.Controllers;
using AprendendoNovaVersao.Interface;
using AprendendoNovaVersao.Model;
using AprendendoNovaVersao.Negocio;
using AprendendoNovaVersao.Persistencia;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoTestes
{
    public class AutenticacaoUnitTestController
    {

        private IAutenticacaoNegocio _autenticacao;

        public static DbContextOptions<TesteContext> dbContextOptions { get; }
        public static string connectionString = "Server=localhost,1433;DataBase=TesteDB;Trusted_Connection=True;";

        static AutenticacaoUnitTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<TesteContext>().UseSqlServer(connectionString).Options;
        }

        public AutenticacaoUnitTestController()
        {
            var context = new TesteContext(dbContextOptions);
            var padraoBD = new PadraoBD(context);

            var builder = new ConfigurationBuilder().AddJsonFile($"testappsettings.json", optional: false);
            var configuration = builder.Build();

            _autenticacao = new AutenticacaoNegocio(padraoBD,configuration);
        }

        [Fact]
        public void PostCreateToken_Return_OkResult()
        {
            var controller = new AutenticacaoController(_autenticacao);
            Usuario usuario = new Usuario()
            {
                NomeUsuario = "Cristhian",
                Senha = "senha"
            };
            var data = controller.CreateToken(usuario);

            Assert.IsType<string>(data.Value);
        }

        [Fact]
        public void PostCreateToken_Return_UnauthorizedResult()
        {
            var controller = new AutenticacaoController(_autenticacao);
            Usuario usuario = new Usuario()
            {
                NomeUsuario = "Cristhian",
                Senha = "senhaErrada"
            };
            var data = controller.CreateToken(usuario);

            Assert.IsType<UnauthorizedResult>(data.Result);
        }

        [Fact]
        public void PostCreateToken_Return_BadRequestResult()
        {
            var controller = new AutenticacaoController(_autenticacao);
            Usuario usuario = new Usuario()
            {
                NomeUsuario = "",
                Senha = "senha"
            };
            var data = controller.CreateToken(usuario);

            Assert.IsType<BadRequestObjectResult>(data.Result);
        }
    }
}
