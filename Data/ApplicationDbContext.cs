using Microsoft.EntityFrameworkCore;
using c_.Models;

namespace c_.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Descricao).HasMaxLength(1000);
                entity.Property(e => e.Preco).HasColumnType("decimal(18,2)");
                entity.Property(e => e.DataCadastro).HasDefaultValueSql("CURRENT_TIMESTAMP AT TIME ZONE 'UTC'");
                entity.Property(e => e.Ativo).HasDefaultValue(true);
            });
        }
    }
}
