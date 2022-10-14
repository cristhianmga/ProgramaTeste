using AprendendoNovaVersao.Model;
using Microsoft.AspNetCore.Mvc;

namespace AprendendoNovaVersao.Interface
{
    public interface IRelatorioMontagem
    {
        public ActionResult<Relatorio> MontarRelatorio(DateTime data);
    }
}
