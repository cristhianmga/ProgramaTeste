using AprendendoNovaVersao.Controllers;
using AprendendoNovaVersao.Interface;
using AprendendoNovaVersao.Model;
using AprendendoNovaVersao.Negocio;
using AprendendoNovaVersao.Persistencia;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoTestes
{
    public class UsuarioUnitTesteController
    {
        private IUsuarioNegocio _usuarioNegocio;

        public static DbContextOptions<TesteContext> dbContextOptions { get; }
        public static string connectionString = "Server=localhost,1433;DataBase=TesteDB;Trusted_Connection=True;";

        static UsuarioUnitTesteController()
        {
            dbContextOptions = new DbContextOptionsBuilder<TesteContext>().UseSqlServer(connectionString).Options;
        }

        public UsuarioUnitTesteController()
        {
            var context = new TesteContext(dbContextOptions);
            var padraoBD = new PadraoBD(context);
            _usuarioNegocio = new UsuarioNegocio(padraoBD);
        }

        [Fact]
        public void PostSalvar_Return_OkResult()
        {
            var controller = new UsuarioController(_usuarioNegocio);
            var usuario = new Usuario()
            {
                Id = 0,
                NomeUsuario = "NomeGenerico",
                Senha = "senha"
            };
            var data = controller.Salvar(usuario);
            Assert.IsType<Usuario>(data.Value);
        }

        [Fact]
        public void PostSalvar_Return_BadRequestResult()
        {
            var controller = new UsuarioController(_usuarioNegocio);
            var usuario = new Usuario()
            {
                Id = 3,
                NomeUsuario = "NomeGenerico",
                Senha = "senha"
            };
            var data = controller.Salvar(usuario);
            Assert.IsType<BadRequestObjectResult>(data.Result);
        }
    }
}
