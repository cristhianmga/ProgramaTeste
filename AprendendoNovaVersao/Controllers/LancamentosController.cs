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
        private readonly ILancamentoNegocio _lancamento;

        public LancamentosController(ILancamentoNegocio lancamento)
        {
            _lancamento = lancamento;
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
        ///         "Valor": 17.50
        ///     }
        /// </remarks>
        /// <param name="lancamento">Objeto lancamento</param>
        /// <returns>O lançamento incluído</returns>
        /// <remarks>Retorna o lançamento incluído</remarks>
        [Produces("application/json")]
        [HttpPost]
        [Authorize]
        public ActionResult<Lancamento> Salvar(Lancamento lancamento)
        {
            try
            {
                return _lancamento.SalvarLancamento(lancamento);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// <summary>
        /// Deleta um lançamento utilizando o ID
        /// </summary>
        /// <param name="id">Id do lançamento</param>
        /// <returns>Retorna uma mensagem</returns>
        /// <remarks>Retorna se foi concluida a exlusão</remarks>
        [HttpDelete]
        [Authorize]
        public ActionResult<string> Deletar(int id)
        {
            try
            {
                return _lancamento.DeletarLancamento(id);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
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
        ///         "Valor": 17.50
        ///     }
        /// </remarks>
        /// <param name="lancamento">Objeto lancamento</param>
        /// <returns>Objeto lançamento</returns>
        /// <remarks>Retorna o lançamento atulizado</remarks>
        [Produces("application/json")]
        [HttpPut]
        [Authorize]
        public ActionResult<Lancamento> Atualizar(Lancamento lancamento)
        {
            try
            {
                return _lancamento.AtualizarLancamento(lancamento);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Obtem todos os lançamentos feitos no banco.
        /// </summary>
        /// <returns>Lista de lançamentos</returns>
        /// <remarks>Retorna todos os laçamentos do banco</remarks>
        [Produces("application/json")]
        [HttpGet]
        [Authorize]
        public ActionResult<List<Lancamento>> ObterTodos()
        {
            try
            {
                return _lancamento.ObterTodosLancamento();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
