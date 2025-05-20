using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prueba_Tecnica.Migrations
{
    /// <inheritdoc />
    public partial class TercerIntento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReservaApartamento",
                columns: table => new
                {
                    IdReserva = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdApartamento = table.Column<int>(type: "int", nullable: false),
                    IdTemporada = table.Column<int>(type: "int", nullable: false),
                    FechaLlegada = table.Column<DateTime>(type: "date", nullable: false),
                    FechaSalida = table.Column<DateTime>(type: "date", nullable: false),
                    Noches = table.Column<int>(type: "int", nullable: false),
                    NumeroPersonas = table.Column<int>(type: "int", nullable: false),
                    IncluyeLavanderia = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PrecioTotal = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservaApartamento", x => x.IdReserva);
                    table.ForeignKey(
                        name: "FK_ReservaApartamento_Apartamento",
                        column: x => x.IdApartamento,
                        principalTable: "Apartamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservaApartamento_Temporada",
                        column: x => x.IdTemporada,
                        principalTable: "Temporada",
                        principalColumn: "IdTemporada",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservaApartamento_IdApartamento",
                table: "ReservaApartamento",
                column: "IdApartamento");

            migrationBuilder.CreateIndex(
                name: "IX_ReservaApartamento_IdTemporada",
                table: "ReservaApartamento",
                column: "IdTemporada");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservaApartamento");
        }
    }
}
