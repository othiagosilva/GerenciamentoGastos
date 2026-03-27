using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gerenciamento.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    idCategoria = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    finalidade = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.idCategoria);
                });

            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    idPessoa = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    idade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.idPessoa);
                });

            migrationBuilder.CreateTable(
                name: "Transacoes",
                columns: table => new
                {
                    idTransacao = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idCategoria = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    categoriaidCategoria = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    idPessoa = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacoes", x => x.idTransacao);
                    table.ForeignKey(
                        name: "FK_Transacoes_Categorias_categoriaidCategoria",
                        column: x => x.categoriaidCategoria,
                        principalTable: "Categorias",
                        principalColumn: "idCategoria",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transacoes_Pessoas_idPessoa",
                        column: x => x.idPessoa,
                        principalTable: "Pessoas",
                        principalColumn: "idPessoa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_categoriaidCategoria",
                table: "Transacoes",
                column: "categoriaidCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_idPessoa",
                table: "Transacoes",
                column: "idPessoa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transacoes");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Pessoas");
        }
    }
}
