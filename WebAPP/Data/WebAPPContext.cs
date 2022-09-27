using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public DbSet<WebAPP.Models.Cliente> Cliente { get; set; } = default!;
    }
}
