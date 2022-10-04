using AprendendoNovaVersao.Model;
using Microsoft.EntityFrameworkCore;

namespace AprendendoNovaVersao
{
    public class TesteContext : DbContext
    {
        public TesteContext(DbContextOptions<TesteContext> options) : base(options)
        {

        }

        public DbSet<Lancamento> Lancamentos { get; set; }
    }
}
