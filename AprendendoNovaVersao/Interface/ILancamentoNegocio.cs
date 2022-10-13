using AprendendoNovaVersao.Model;

namespace AprendendoNovaVersao.Interface
{
    public interface ILancamentoNegocio
    {
        public Lancamento SalvarLancamento(Lancamento lancamento);
        public Lancamento AtualizarLancamento(Lancamento lancamento);
        public string DeletarLancamento(int id);
        public List<Lancamento> ObterTodosLancamento();
    }
}
