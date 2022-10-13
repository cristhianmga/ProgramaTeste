using AprendendoNovaVersao.Interface;
using AprendendoNovaVersao.Model;

namespace AprendendoNovaVersao.Negocio
{
    public class LancamentoNegocio : ILancamentoNegocio
    {
        private readonly IPadraoBD _padraoBD;
        public LancamentoNegocio(IPadraoBD padraoBD)
        {
            _padraoBD = padraoBD;
        }

        public Lancamento SalvarLancamento(Lancamento lancamento)
        {
            if (lancamento.Id != 0)
            {
                throw new Exception("O campo Id não deve estar preenchido.");
            }
            return _padraoBD.Salvar(lancamento);
        }
        public Lancamento AtualizarLancamento(Lancamento lancamento)
        {
            if (lancamento.Id == 0)
            {
                throw new Exception("O campo Id deve estar preenchido.");
            }
            return _padraoBD.Atualizar(lancamento);
        }
        public string DeletarLancamento(int id)
        {
            return _padraoBD.Excluir<Lancamento>(id);
        }
        public List<Lancamento> ObterTodosLancamento()
        {
            return _padraoBD.ObterTodos<Lancamento>().ToList();
        }
    }
}
