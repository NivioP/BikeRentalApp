using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeRentalApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEntregadoresTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entregadores",
                columns: table => new
                {
                    Identificador = table.Column<string>(type: "text", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    CNPJ = table.Column<string>(type: "text", nullable: false),
                    Data_Nascimento = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Numero_CNH = table.Column<string>(type: "text", nullable: false),
                    Tipo_CNH = table.Column<string>(type: "text", nullable: false),
                    Imagem_CNH = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entregadores", x => x.Identificador);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entregadores_CNPJ",
                table: "Entregadores",
                column: "CNPJ",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Entregadores_Numero_CNH",
                table: "Entregadores",
                column: "Numero_CNH",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entregadores");
        }
    }
}
