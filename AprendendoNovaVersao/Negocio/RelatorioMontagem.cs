using AprendendoNovaVersao.Interface;
using AprendendoNovaVersao.Model;

namespace AprendendoNovaVersao.Negocio
{
    public class RelatorioMontagem : IRelatorioMontagem
    {
        private readonly IPadraoBD _padraoBD;
        public RelatorioMontagem(IPadraoBD padraoBD)
        {
            _padraoBD = padraoBD;
        }

        public Relatorio MontarRelatorio(DateTime data)
        {
            Relatorio relatorio = new Relatorio();
            relatorio.DataVenda = data;
            var lancamentos = _padraoBD.ObterTodos<Lancamento>().Where(lancamento => lancamento.Data.Date == data.Date);
            foreach (Lancamento item in lancamentos)
            {
                relatorio.SaldoDiario += item.Valor;
            }

            relatorio.Mensagem = string.Format("O saldo do dia {0} é : {1}", relatorio.DataVenda.Date, relatorio.SaldoDiario);

            return relatorio;
        }
    }
}
