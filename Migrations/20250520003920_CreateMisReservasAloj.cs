using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prueba_Tecnica.Migrations
{
    /// <inheritdoc />
    public partial class CreateMisReservasAloj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MisReservasAloj",
                columns: table => new
                {
                    IdReserva = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAlojamiento = table.Column<int>(type: "int", nullable: false),
                    IdTarifa = table.Column<int>(type: "int", nullable: false),
                    IdTemporada = table.Column<int>(type: "int", nullable: false),
                    FechaLlegada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaSalida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    NumeroPersonas = table.Column<int>(type: "int", nullable: false),
                    IncluyeServicios = table.Column<bool>(type: "bit", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MisReservasAloj", x => x.IdReserva);
                    table.ForeignKey(
                        name: "FK_MisReservasAloj_Alojamiento",
                        column: x => x.IdAlojamiento,
                        principalTable: "Alojamiento",
                        principalColumn: "IdAlojamiento",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MisReservasApt_TarifaAlojamiento",
                        column: x => x.IdTarifa,
                        principalTable: "TarifaAlojamiento",
                        principalColumn: "IdTarifa",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MisReservasApt_Temporada",
                        column: x => x.IdTemporada,
                        principalTable: "Temporada",
                        principalColumn: "IdTemporada",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MisReservasAloj_IdAlojamiento",
                table: "MisReservasAloj",
                column: "IdAlojamiento");

            migrationBuilder.CreateIndex(
                name: "IX_MisReservasAloj_IdTarifa",
                table: "MisReservasAloj",
                column: "IdTarifa");

            migrationBuilder.CreateIndex(
                name: "IX_MisReservasAloj_IdTemporada",
                table: "MisReservasAloj",
                column: "IdTemporada");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MisReservasAloj");
        }
    }
}
