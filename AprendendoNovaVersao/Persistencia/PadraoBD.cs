using AprendendoNovaVersao.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AprendendoNovaVersao.Persistencia
{
    public class PadraoBD : IPadraoBD
    {
        private readonly TesteContext _context;

        public PadraoBD(TesteContext context)
        {
            _context = context;
        }


        public E Salvar<E>(E entity) where E : class
        {
            _context.Entry(entity).State = EntityState.Added;
            _context.SaveChanges();
            return entity;
        }
        public E Atualizar<E>(E entity) where E : class
        {
            _context.Set<E>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }
        public string Excluir<E>(int id) where E : class
        {
            E? entity = _context.Set<E>().Find(id);
            if (entity == null)
            {
                throw new Exception("Objeto não encontrado");
            }
            else
            {
                _context.Set<E>().Remove(entity);
                _context.Entry(entity).State = EntityState.Deleted;
                _context.SaveChanges();
                return "Objeto Excluido";
            }
        }

        public IQueryable<E> ObterTodos<E>() where E : class
        {
            return _context.Set<E>().AsQueryable();
        }
    }
}
