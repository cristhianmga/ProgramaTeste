using AprendendoNovaVersao.Interface;
using AprendendoNovaVersao.Model;
using Microsoft.AspNetCore.Mvc;

namespace AprendendoNovaVersao.Negocio
{
    public class RelatorioMontagem : IRelatorioMontagem
    {
        private readonly IPadraoBD _padraoBD;
        public RelatorioMontagem(IPadraoBD padraoBD)
        {
            _padraoBD = padraoBD;
        }

        public ActionResult<Relatorio> MontarRelatorio(DateTime data)
        {
            Relatorio relatorio = new Relatorio();
            var lancamentos = _padraoBD.ObterTodos<Lancamento>().Where(lancamento => lancamento.Data.Date == data.Date);

            if(lancamentos.Count() == 0)
            {
                throw new Exception();
            }

            foreach (Lancamento item in lancamentos)
            {

                relatorio.Mensagem += string.Format("\n{0} R${1}", item.Descricao, item.Valor);
                relatorio.SaldoDiario += item.Valor;
            }

            relatorio.DataVenda = data;
            relatorio.Mensagem += string.Format("\n--------------------");
            relatorio.Mensagem += string.Format("\nO saldo diario: R${0}", relatorio.SaldoDiario);

            return relatorio;
        }
    }
}
