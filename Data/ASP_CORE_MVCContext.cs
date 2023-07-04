using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ASP_CORE_MVC.Models;

namespace ASP_CORE_MVC.Data {
    /// <summary>
    /// Database Context Class
    /// </summary>
    public class ASP_CORE_MVCContext : DbContext {
        public ASP_CORE_MVCContext(DbContextOptions<ASP_CORE_MVCContext> options)
            : base(options) {
        }

        /*Can used to query and save instances of TEntity.*/
        public DbSet<ASP_CORE_MVC.Models.Movie> Movie { get; set; } = default!;
    }
}
