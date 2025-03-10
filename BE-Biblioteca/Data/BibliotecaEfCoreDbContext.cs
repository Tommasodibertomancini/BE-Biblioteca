using BE_Biblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace BE_Biblioteca.Data
{
    public class BibliotecaEfCoreDbContext : DbContext
    {
        public BibliotecaEfCoreDbContext(DbContextOptions<BibliotecaEfCoreDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
    }
}
