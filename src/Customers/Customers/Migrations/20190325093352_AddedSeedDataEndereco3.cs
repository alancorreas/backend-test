using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Customers.Migrations
{
    public partial class AddedSeedDataEndereco3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cliente",
                keyColumn: "Id",
                keyValue: new Guid("3f1be2c4-b509-47a2-a097-93aef7d337b6"));

            migrationBuilder.DeleteData(
                table: "Cliente",
                keyColumn: "Id",
                keyValue: new Guid("b815c0a7-f0a6-4943-aa08-180a992e838e"));

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Endereco");

            migrationBuilder.InsertData(
                table: "Endereco",
                columns: new[] { "Id", "CEP", "Cidade", "ClienteForeignKey", "Complemento", "Logradouro", "Numero", "UF" },
                values: new object[] { new Guid("b815c0a7-f0a6-4943-aa08-180a992e838e"), "81000000", "Curitiba", new Guid("d201f4ab-46e7-48b7-80d1-bfd5569f1316"), "Ap 1 Bl 1s", "Rua de Teste", "999", "PR" });

            migrationBuilder.InsertData(
                table: "Endereco",
                columns: new[] { "Id", "CEP", "Cidade", "ClienteForeignKey", "Complemento", "Logradouro", "Numero", "UF" },
                values: new object[] { new Guid("3f1be2c4-b509-47a2-a097-93aef7d337b6"), "06100000", "São Paulo", new Guid("5cb5a9bc-6ae7-43ed-ac6f-0ca70376cc3c"), null, "Avenida de Teste", "1", "SP" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Endereco",
                keyColumn: "Id",
                keyValue: new Guid("3f1be2c4-b509-47a2-a097-93aef7d337b6"));

            migrationBuilder.DeleteData(
                table: "Endereco",
                keyColumn: "Id",
                keyValue: new Guid("b815c0a7-f0a6-4943-aa08-180a992e838e"));

            migrationBuilder.AddColumn<Guid>(
                name: "ClienteId",
                table: "Endereco",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Cliente",
                columns: new[] { "Id", "CodigoArea", "Email", "Nome", "Telefone" },
                values: new object[] { new Guid("b815c0a7-f0a6-4943-aa08-180a992e838e"), null, null, null, null });

            migrationBuilder.InsertData(
                table: "Cliente",
                columns: new[] { "Id", "CodigoArea", "Email", "Nome", "Telefone" },
                values: new object[] { new Guid("3f1be2c4-b509-47a2-a097-93aef7d337b6"), null, null, null, null });
        }
    }
}
