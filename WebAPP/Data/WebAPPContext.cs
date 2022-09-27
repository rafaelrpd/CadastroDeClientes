using Microsoft.EntityFrameworkCore;
using WebAPP.Models;

namespace WebAPP.Data
{
    public class WebAPPContext : DbContext
    {
        public WebAPPContext (DbContextOptions<WebAPPContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Cliente { get; set; } = default!;
        public DbSet<Cidade> Cidade { get; set; } = default!;
        public DbSet<Estado> Estado { get; set; } = default!;
    }
}
