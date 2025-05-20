using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prueba_Tecnica.Migrations
{
    /// <inheritdoc />
    public partial class Alojamientos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MisReservasApt_Estado",
                table: "MisReservasApt");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "MisReservasApt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "MisReservasApt",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                defaultValue: "Pendiente");

            migrationBuilder.CreateIndex(
                name: "IX_MisReservasApt_Estado",
                table: "MisReservasApt",
                column: "Estado");
        }
    }
}
