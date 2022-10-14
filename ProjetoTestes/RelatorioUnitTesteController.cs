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
    public class RelatorioUnitTesteController
    {
        private IRelatorioMontagem _relatorio;

        public static DbContextOptions<TesteContext> dbContextOptions { get; }
        public static string connectionString = "Server=localhost,1433;DataBase=TesteDB;Trusted_Connection=True;";

        static RelatorioUnitTesteController()
        {
            dbContextOptions = new DbContextOptionsBuilder<TesteContext>().UseSqlServer(connectionString).Options;
        }

        public RelatorioUnitTesteController()
        {
            var context = new TesteContext(dbContextOptions);
            var padraoBD = new PadraoBD(context);
            _relatorio = new RelatorioMontagem(padraoBD);
        }

        [Fact]
        public void PostSalvar_Return_OkResult()
        {
            var controller = new RelatorioController(_relatorio);
            var data = controller.ObterRelatorioPorDia(new DateTime(2022, 10, 13));
            Assert.IsType<Relatorio>(data.Value);
        }

        [Fact]
        public void PostSalvar_Return_NotFoundResult()
        {
            var controller = new RelatorioController(_relatorio);
            var data = controller.ObterRelatorioPorDia(new DateTime(2022, 10, 14));
            Assert.IsType<NotFoundResult>(data.Result);
        }
    }
}
