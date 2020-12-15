using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.DBConfiguration.EFCore
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<PedidoItem> PedidoItem { get; set; }

        public ApplicationContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlite(DatabaseConnection.ConnectionConfiguration.GetConnectionString("DefaultConnection"));
            }
        }

        // Criando o DatabaseContext por Dependency Injection.
        // Vai ser utilizado pela camada de Presentation para configurar o banco via Dependency Injection.
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pedido>()
            .HasData(
                new Pedido
                {
                    Id = 1,
                    NumeroDoPedido = 123456
                }
            );
            modelBuilder.Entity<PedidoItem>()
            .HasData(
                new PedidoItem
                {
                    Id = 1,
                    Descricao = "Item A",
                    PrecoUnitario = 10,
                    Quantidade = 1,
                    PedidoId = 1
                }
            );
            modelBuilder.Entity<PedidoItem>()
            .HasData(
                new PedidoItem
                {
                    Id = 2,
                    Descricao = "Item B",
                    PrecoUnitario = 5,
                    Quantidade = 2,
                    PedidoId = 1
                }
            );
        }
    }
}
