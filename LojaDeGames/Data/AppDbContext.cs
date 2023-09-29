using LojaDeGames.Model;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace LojaDeGames.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //MODEL GERA AS TABELAS
            modelBuilder.Entity<Produto>().ToTable("tb_categorias");
            modelBuilder.Entity<Categoria>().ToTable("tb_temas");

            _ = modelBuilder.Entity<Produto>()
                .HasOne(_ => _.Categoria)
                .WithMany(t => t.Produto)
                .HasForeignKey("CategoriaId")
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Categoria> Categorias { get; set; } = null!;

        public DbSet<Produto> Produtos { get; set; } = null!;

    }
}

