using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class PopularDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Pedido",
                columns: new[] { "Id", "NumeroDoPedido" },
                values: new object[] { 1, 123456 });

            migrationBuilder.InsertData(
                table: "PedidoItem",
                columns: new[] { "Id", "Descricao", "PedidoId", "PrecoUnitario", "Quantidade" },
                values: new object[] { 1, "Item A", 1, 10.0, 1 });

            migrationBuilder.InsertData(
                table: "PedidoItem",
                columns: new[] { "Id", "Descricao", "PedidoId", "PrecoUnitario", "Quantidade" },
                values: new object[] { 2, "Item B", 1, 5.0, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PedidoItem",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PedidoItem",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Pedido",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
