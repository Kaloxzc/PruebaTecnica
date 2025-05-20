using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prueba_Tecnica.Migrations
{
    /// <inheritdoc />
    public partial class AddMisReservasAptConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MisReservasApt",
                columns: table => new
                {
                    IdReserva = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdApartamento = table.Column<int>(type: "int", nullable: false),
                    IdTarifa = table.Column<int>(type: "int", nullable: false),
                    FechaLlegada = table.Column<DateTime>(type: "date", nullable: false),
                    FechaSalida = table.Column<DateTime>(type: "date", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    Estado = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, defaultValue: "Pendiente")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MisReservasApt", x => x.IdReserva);
                    table.CheckConstraint("CHK_MisReservasApt_EstadoValido", "Estado IN ('Pendiente', 'Confirmada', 'Cancelada')");
                    table.CheckConstraint("CHK_MisReservasApt_FechasValidas", "FechaLlegada < FechaSalida");
                    table.ForeignKey(
                        name: "FK_MisReservasApt_Apartamento",
                        column: x => x.IdApartamento,
                        principalTable: "Apartamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MisReservasApt_TarifaApartamento",
                        column: x => x.IdTarifa,
                        principalTable: "TarifaApartamento",
                        principalColumn: "IdTarifa",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MisReservasApt_ApartamentoFechas",
                table: "MisReservasApt",
                columns: new[] { "IdApartamento", "FechaLlegada", "FechaSalida" });

            migrationBuilder.CreateIndex(
                name: "IX_MisReservasApt_Estado",
                table: "MisReservasApt",
                column: "Estado");

            migrationBuilder.CreateIndex(
                name: "IX_MisReservasApt_IdTarifa",
                table: "MisReservasApt",
                column: "IdTarifa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MisReservasApt");
        }
    }
}
