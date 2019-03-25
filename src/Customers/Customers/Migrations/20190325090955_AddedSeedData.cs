using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Customers.Migrations
{
    public partial class AddedSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cliente",
                columns: new[] { "Id", "CodigoArea", "Email", "Nome", "Telefone" },
                values: new object[,]
                {
                    { new Guid("d201f4ab-46e7-48b7-80d1-bfd5569f1316"), null, "cliente1@email.com", "Cliente1", null },
                    { new Guid("5ba32385-0894-4589-85d5-8c83f924711c"), 41, "cliente1@email2.com", "Cliente2", "999999999" },
                    { new Guid("5cb5a9bc-6ae7-43ed-ac6f-0ca70376cc3c"), 11, "cliente1@email3.com", "Cliente3", "888888888" },
                    { new Guid("b815c0a7-f0a6-4943-aa08-180a992e838e"), null, null, null, null },
                    { new Guid("3f1be2c4-b509-47a2-a097-93aef7d337b6"), null, null, null, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cliente",
                keyColumn: "Id",
                keyValue: new Guid("3f1be2c4-b509-47a2-a097-93aef7d337b6"));

            migrationBuilder.DeleteData(
                table: "Cliente",
                keyColumn: "Id",
                keyValue: new Guid("5ba32385-0894-4589-85d5-8c83f924711c"));

            migrationBuilder.DeleteData(
                table: "Cliente",
                keyColumn: "Id",
                keyValue: new Guid("5cb5a9bc-6ae7-43ed-ac6f-0ca70376cc3c"));

            migrationBuilder.DeleteData(
                table: "Cliente",
                keyColumn: "Id",
                keyValue: new Guid("b815c0a7-f0a6-4943-aa08-180a992e838e"));

            migrationBuilder.DeleteData(
                table: "Cliente",
                keyColumn: "Id",
                keyValue: new Guid("d201f4ab-46e7-48b7-80d1-bfd5569f1316"));
        }
    }
}
