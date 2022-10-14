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
        [ProducesResponseType(typeof(ActionResult<Relatorio>), 200)]
        [ProducesResponseType(typeof(ActionResult), 404)]
        [Authorize]
        public ActionResult<Relatorio> ObterRelatorioPorDia(DateTime dataVenda)
        {
            try
            {
                return _relatorio.MontarRelatorio(dataVenda);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
