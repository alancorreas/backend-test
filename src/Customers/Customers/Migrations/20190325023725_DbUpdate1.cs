using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Customers.Migrations
{
    public partial class DbUpdate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CodigoArea",
                table: "Cliente",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Cliente",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Cliente",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Cliente",
                maxLength: 15,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Logradouro = table.Column<string>(maxLength: 50, nullable: true),
                    Numero = table.Column<string>(maxLength: 10, nullable: true),
                    Complemento = table.Column<string>(maxLength: 20, nullable: true),
                    CEP = table.Column<string>(maxLength: 8, nullable: true),
                    Cidade = table.Column<string>(maxLength: 30, nullable: true),
                    UF = table.Column<string>(maxLength: 2, nullable: true),
                    ClienteForeignKey = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Endereco_Cliente_ClienteForeignKey",
                        column: x => x.ClienteForeignKey,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_ClienteForeignKey",
                table: "Endereco",
                column: "ClienteForeignKey",
                unique: true,
                filter: "[ClienteForeignKey] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.DropColumn(
                name: "CodigoArea",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Cliente");
        }
    }
}
