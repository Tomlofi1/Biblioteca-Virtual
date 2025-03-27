using BibliotecaVirtual.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaVirtual.Data
{
    public class ContextoData : DbContext
    {
        public DbSet<Livros> Livros { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Funcionarios> Funcionarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ClienteLivro> ClienteLivros { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder.UseNpgsql(
                "Server=localhost;" +
                "Port=5432;" +
                "User Id=postgres;" +
                "Password=123"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClienteLivro>()
                .HasKey(pc => new { pc.LivroId, pc.ClienteId });
            modelBuilder.Entity<ClienteLivro>()
                .HasOne(p => p.Livros)
                .WithMany(l => l.ClienteLivros)
                .HasForeignKey(o => o.LivroId);

            modelBuilder.Entity<ClienteLivro>()
                .HasOne(o => o.Clientes)
                .WithMany(l => l.ClienteLivros)
                .HasForeignKey(o => o.ClienteId);
        }
    }
}
