using Gerenciamento.Api.Models;
using Microsoft.EntityFrameworkCore;
namespace Gerenciamento.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
   
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pessoa>(entity =>
            {
                entity.HasKey(e => e.idPessoa);
                entity.Property(e => e.nome).IsRequired().HasMaxLength(200);
                entity.Property(e => e.idade).IsRequired();

                entity.HasMany(p => p.transacoes)
                      .WithOne(t => t.pessoa)
                      .HasForeignKey(t => t.idPessoa)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(c => c.idCategoria);
                entity.Property(c => c.descricao).IsRequired().HasMaxLength(400);
                entity.Property(c => c.finalidade).IsRequired(); // despesa/receita/ambas
            });

            modelBuilder.Entity<Transacao>(entity =>
            {
                entity.HasKey(t => t.idTransacao);
                entity.Property(t => t.descricao).IsRequired().HasMaxLength(400);
                entity.Property(t => t.valor).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(t => t.tipo).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
