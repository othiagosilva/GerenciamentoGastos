using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gerenciamento.Api.Migrations
{
    /// <inheritdoc />
    public partial class CorrigindoRelacionamentoTransacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacoes_Categorias_categoriaidCategoria",
                table: "Transacoes");

            migrationBuilder.DropIndex(
                name: "IX_Transacoes_categoriaidCategoria",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "categoriaidCategoria",
                table: "Transacoes");

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_idCategoria",
                table: "Transacoes",
                column: "idCategoria");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacoes_Categorias_idCategoria",
                table: "Transacoes",
                column: "idCategoria",
                principalTable: "Categorias",
                principalColumn: "idCategoria",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacoes_Categorias_idCategoria",
                table: "Transacoes");

            migrationBuilder.DropIndex(
                name: "IX_Transacoes_idCategoria",
                table: "Transacoes");

            migrationBuilder.AddColumn<Guid>(
                name: "categoriaidCategoria",
                table: "Transacoes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_categoriaidCategoria",
                table: "Transacoes",
                column: "categoriaidCategoria");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacoes_Categorias_categoriaidCategoria",
                table: "Transacoes",
                column: "categoriaidCategoria",
                principalTable: "Categorias",
                principalColumn: "idCategoria",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
