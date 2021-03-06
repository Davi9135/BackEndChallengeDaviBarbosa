﻿// <auto-generated />
using Infrastructure.DBConfiguration.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20201213014600_PopularDataBase")]
    partial class PopularDataBase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Domain.Entities.Pedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumeroDoPedido")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Pedido");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            NumeroDoPedido = 123456
                        });
                });

            modelBuilder.Entity("Domain.Entities.PedidoItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descricao")
                        .HasColumnType("TEXT");

                    b.Property<int>("PedidoId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("PrecoUnitario")
                        .HasColumnType("REAL");

                    b.Property<int>("Quantidade")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PedidoId");

                    b.ToTable("PedidoItem");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Descricao = "Item A",
                            PedidoId = 1,
                            PrecoUnitario = 10.0,
                            Quantidade = 1
                        },
                        new
                        {
                            Id = 2,
                            Descricao = "Item B",
                            PedidoId = 1,
                            PrecoUnitario = 5.0,
                            Quantidade = 2
                        });
                });

            modelBuilder.Entity("Domain.Entities.PedidoItem", b =>
                {
                    b.HasOne("Domain.Entities.Pedido", "Pedido")
                        .WithMany("PedidoItems")
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pedido");
                });

            modelBuilder.Entity("Domain.Entities.Pedido", b =>
                {
                    b.Navigation("PedidoItems");
                });
#pragma warning restore 612, 618
        }
    }
}
