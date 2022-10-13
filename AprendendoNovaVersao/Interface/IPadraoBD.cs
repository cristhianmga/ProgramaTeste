namespace AprendendoNovaVersao.Interface
{
    public interface IPadraoBD
    {
        public E Salvar<E>(E entity) where E : class;
        public E Atualizar<E>(E entity) where E : class;
        public string Excluir<E>(int id) where E : class;
        public IQueryable<E> ObterTodos<E>() where E : class;
    }
}
