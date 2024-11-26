using Microsoft.EntityFrameworkCore;
using zdzcode_api.Model;

namespace zdzcode_api.Repository
{
    public class ContextBase : DbContext
    {
        public DbSet<Produtos> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        public ContextBase(DbContextOptions<ContextBase> options) : base(options)
        {
        }
    }
}
