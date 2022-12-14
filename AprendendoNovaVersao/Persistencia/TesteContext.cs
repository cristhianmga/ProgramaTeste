using AprendendoNovaVersao.Model;
using Microsoft.EntityFrameworkCore;

namespace AprendendoNovaVersao.Persistencia
{
    public class TesteContext : DbContext
    {
        public TesteContext(DbContextOptions<TesteContext> options) : base(options)
        {

        }

        public DbSet<Lancamento> Lancamentos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
