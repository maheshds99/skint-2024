using Microsoft.EntityFrameworkCore;
using skinet.Models.Entities;

namespace skinet.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
         
        }
        public DbSet<Product> Products { get; set; }


    }
}
