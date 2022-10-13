using AprendendoNovaVersao.Interface;
using AprendendoNovaVersao.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AprendendoNovaVersao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LancamentosController : ControllerBase
    {
        private readonly IPadraoBD _padraoBD;

        public LancamentosController(IPadraoBD padraoBD)
        {
            _padraoBD = padraoBD;
        }

        /// <summary>
        /// Cria um lançamento para o dia especificado
        /// </summary>
        /// <remarks>
        /// Exemplo de Request
        /// 
        ///     POST 
        ///     {
        ///         "Descricao": "Descrição do produto"
        ///         "Data": "2022-01-12"
        ///         "Valor": 17,50
        ///     }
        /// </remarks>
        /// <param name="lancamento">Objeto lancamento</param>
        /// <returns>O lançamento incluído</returns>
        /// <remarks>Retorna o lançamento incluído</remarks>
        [Produces("application/json")]
        [HttpPost]
        [Authorize]
        public Lancamento Salvar(Lancamento lancamento)
        {
            return _padraoBD.Salvar(lancamento);
        }


        /// <summary>
        /// Deleta um lançamento utilizando o ID
        /// </summary>
        /// <param name="id">Id do lançamento</param>
        /// <returns>Retorna uma mensagem</returns>
        /// <remarks>Retorna se foi concluida a exlusão</remarks>
        [HttpDelete]
        [Authorize]
        public string Deletar(int id)
        {
            return _padraoBD.Excluir<Lancamento>(id);
        }


        /// <summary>
        /// Atualiza um lançamento
        /// </summary>
        /// <remarks>
        /// Exemplo de Request
        /// 
        ///     PUT 
        ///     {
        ///         "Id": 5 
        ///         "Descricao": "Descrição do produto"
        ///         "Data": "2022-01-12"
        ///         "Valor": 17,50
        ///     }
        /// </remarks>
        /// <param name="lancamento">Objeto lancamento</param>
        /// <returns>Objeto lançamento</returns>
        /// <remarks>Retorna o lançamento atulizado</remarks>
        [Produces("application/json")]
        [HttpPut]
        [Authorize]
        public Lancamento Atualizar(Lancamento lancamento)
        {
            return _padraoBD.Atualizar(lancamento);
        }
        /// <summary>
        /// Obtem todos os lançamentos feitos no banco.
        /// </summary>
        /// <returns>Lista de lançamentos</returns>
        /// <remarks>Retorna todos os laçamentos do banco</remarks>
        [Produces("application/json")]
        [HttpGet]
        [Authorize]
        public List<Lancamento> ObterTodos()
        {
            return _padraoBD.ObterTodos<Lancamento>().ToList();
        }




        //[HttpGet("obterRelatorioPorDia")]
        //public Relatorio ObterRelatorioPorDia(DateTime dataVenda)
        //{
        //    Relatorio relatorio = new Relatorio();
        //    relatorio.DataVenda = dataVenda;
        //    var lancamentos = _context.Lancamentos.Where(lancamento => lancamento.Data.Date == dataVenda.Date);
        //    foreach(Lancamento item in lancamentos)
        //    {
        //        relatorio.SaldoDiario += item.Valor;
        //    }

        //    relatorio.Mensagem = string.Format("O saldo do dia {0} é : {1}", relatorio.DataVenda.Date, relatorio.SaldoDiario);

        //    return relatorio;
        //}
    }
}
