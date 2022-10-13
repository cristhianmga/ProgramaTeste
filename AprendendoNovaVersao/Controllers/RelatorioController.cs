using AprendendoNovaVersao.Interface;
using AprendendoNovaVersao.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AprendendoNovaVersao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatorioController : ControllerBase
    {
        private readonly IRelatorioMontagem _relatorio;
        public RelatorioController(IRelatorioMontagem relatorioMontagem)
        {
            _relatorio = relatorioMontagem;
        }

        [HttpGet("obterPorDia")]
        [Produces("application/json")]
        [Authorize]
        public Relatorio ObterRelatorioPorDia(DateTime dataVenda)
        {
            return _relatorio.MontarRelatorio(dataVenda);
        }
    }
}
