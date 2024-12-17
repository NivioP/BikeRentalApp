using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeRentalApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLocacoesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locacoes",
                columns: table => new
                {
                    Identificador = table.Column<string>(type: "text", nullable: false),
                    Valor_Diaria = table.Column<decimal>(type: "numeric", nullable: false),
                    Entregador_Id = table.Column<string>(type: "text", nullable: false),
                    Moto_Id = table.Column<string>(type: "text", nullable: false),
                    Data_Inicio = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Data_Termino = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Data_Previsao_Termino = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Data_Devolucao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locacoes", x => x.Identificador);
                    table.ForeignKey(
                        name: "FK_Locacoes_Entregadores_Entregador_Id",
                        column: x => x.Entregador_Id,
                        principalTable: "Entregadores",
                        principalColumn: "Identificador",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Locacoes_Motos_Moto_Id",
                        column: x => x.Moto_Id,
                        principalTable: "Motos",
                        principalColumn: "Identificador",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Locacoes_Entregador_Id",
                table: "Locacoes",
                column: "Entregador_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Locacoes_Moto_Id",
                table: "Locacoes",
                column: "Moto_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Locacoes");
        }
    }
}
