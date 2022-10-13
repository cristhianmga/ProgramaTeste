using AprendendoNovaVersao.Controllers;
using AprendendoNovaVersao.Interface;
using AprendendoNovaVersao.Model;
using AprendendoNovaVersao.Negocio;
using AprendendoNovaVersao.Persistencia;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoTestes
{
    public class LancamentoUnitTestController
    {
        private ILancamentoNegocio _lancamento;

        public static DbContextOptions<TesteContext> dbContextOptions{ get; }
        public static string connectionString = "Server=localhost,1433;DataBase=TesteDB;Trusted_Connection=True;";

        static LancamentoUnitTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<TesteContext>().UseSqlServer(connectionString).Options;
        }

        public LancamentoUnitTestController()
        {
            var context = new TesteContext(dbContextOptions);
            var padraoBD = new PadraoBD(context);
            _lancamento = new LancamentoNegocio(padraoBD);
        }

        [Fact]
        public void GetObterTodos_Return_OkResult()
        {
            var controller = new LancamentosController(_lancamento);
            var data = controller.ObterTodos();
            Assert.IsType<List<Lancamento>>(data.Value);
        }

        [Fact]
        public void GetObterTodos_Return_BadRequestResult()
        {
            var controller = new LancamentosController(_lancamento);
            var data = controller.ObterTodos();
            Assert.IsType<BadRequestResult>(data.Result);
        }

        [Fact]
        public void PostSalvar_Return_OkResult()
        {
            var controller = new LancamentosController(_lancamento);
            var lancamento = new Lancamento()
            {
                Id = 0,
                Descricao = "Venda Teste",
                Data = new DateTime(2022, 01, 20),
                Valor = 50
            };

            var retorno = controller.Salvar(lancamento);
            Assert.IsType<Lancamento>(retorno.Value);
        }

        [Fact]
        public void PostSalvar_Return_BadRequestResult()
        {
            var controller = new LancamentosController(_lancamento);
            var lancamento = new Lancamento()
            {
                Id = 5,
                Descricao = "Venda Teste",
                Data = new DateTime(2022, 01, 20),
                Valor = 50
            };

            var retorno = controller.Salvar(lancamento);
            Assert.IsType<BadRequestObjectResult>(retorno.Result);
        }


        [Fact]
        public void PutAtualizar_Return_BadRequestResult()
        {
            var controller = new LancamentosController(_lancamento);
            var lancamento = new Lancamento()
            {
                Id = 0,
                Descricao = "Venda Teste",
                Data = new DateTime(2022, 01, 20),
                Valor = 50
            };

            var retorno = controller.Atualizar(lancamento);
            Assert.IsType<BadRequestObjectResult>(retorno.Result);
        }

        [Fact]
        public void PutAtualizar_Return_OkResult()
        {
            var controller = new LancamentosController(_lancamento);

            var lancamento = new Lancamento()
            {
                Id = 5,
                Descricao = "Venda Teste Atualizada",
                Data = new DateTime(2022, 01, 20),
                Valor = 50
            };

            var retorno = controller.Atualizar(lancamento);
            Assert.IsType<Lancamento>(retorno.Value);
        }

        [Fact]
        public void DeletDeletar_Return_OkResult()
        {
            var controller = new LancamentosController(_lancamento);
            var id = 6;

            var retorno = controller.Deletar(id);
            Assert.IsType<string>(retorno.Value);
        }

        [Fact]
        public void DeletDeletar_Return_BadRequestResult()
        {
            var controller = new LancamentosController(_lancamento);
            var id = 10;

            var retorno = controller.Deletar(id);
            Assert.IsType<NotFoundObjectResult>(retorno.Result);
        }
    }
}
