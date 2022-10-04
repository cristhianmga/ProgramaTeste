using AprendendoNovaVersao.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AprendendoNovaVersao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LancamentosController : ControllerBase
    {
        private readonly TesteContext _context;

        public LancamentosController(TesteContext context)
        {
            _context = context;
        }

        [HttpPost(Name = "Lancamento")]
        public Lancamento Lancamentos(Lancamento lancamento)
        {
            _context.Lancamentos.Add(lancamento);
            _context.SaveChanges();
            return lancamento;
        }
        [HttpGet(Name = "ObterRelatorio")]
        public Relatorio ObterRelatorioPorDia(DateTime dataVenda)
        {
            Relatorio relatorio = new Relatorio();
            relatorio.DataVenda = dataVenda;
            var lancamentos = _context.Lancamentos.Where(lancamento => lancamento.Data.Date == dataVenda.Date);
            foreach(Lancamento item in lancamentos)
            {
                relatorio.SaldoDiario += item.Valor;
            }

            relatorio.Mensagem = string.Format("O saldo do dia {0} é : {1}", relatorio.DataVenda.Date, relatorio.SaldoDiario);

            return relatorio;
        }
    }
}
