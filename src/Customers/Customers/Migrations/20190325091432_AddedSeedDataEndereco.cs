using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Customers.Migrations
{
    public partial class AddedSeedDataEndereco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_Cliente_ClienteForeignKey",
                table: "Endereco");

            migrationBuilder.DropIndex(
                name: "IX_Endereco_ClienteForeignKey",
                table: "Endereco");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClienteForeignKey",
                table: "Endereco",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ClienteId",
                table: "Endereco",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_ClienteForeignKey",
                table: "Endereco",
                column: "ClienteForeignKey",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Endereco_Cliente_ClienteForeignKey",
                table: "Endereco",
                column: "ClienteForeignKey",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_Cliente_ClienteForeignKey",
                table: "Endereco");

            migrationBuilder.DropIndex(
                name: "IX_Endereco_ClienteForeignKey",
                table: "Endereco");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Endereco");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClienteForeignKey",
                table: "Endereco",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_ClienteForeignKey",
                table: "Endereco",
                column: "ClienteForeignKey",
                unique: true,
                filter: "[ClienteForeignKey] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Endereco_Cliente_ClienteForeignKey",
                table: "Endereco",
                column: "ClienteForeignKey",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
