using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prueba_Tecnica.Migrations
{
    /// <inheritdoc />
    public partial class Imagenes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlojamientosCapacidadTotal",
                columns: table => new
                {
                    IdAlojamientosCapacidadTotal = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSede = table.Column<int>(type: "int", nullable: true),
                    CapacidadTotal = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Alojamie__6827F5EDB2C1EC28", x => x.IdAlojamientosCapacidadTotal);
                });

            migrationBuilder.CreateTable(
                name: "ApartamentoCiudad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Apartame__3214EC073BE357A5", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sede",
                columns: table => new
                {
                    IdSede = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CapacidadTotal = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Sede__A7780DFF310ACB83", x => x.IdSede);
                });

            migrationBuilder.CreateTable(
                name: "TarifaLavanderia",
                columns: table => new
                {
                    IdTarifaLavanderia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Precio = table.Column<decimal>(type: "money", nullable: true),
                    Observaciones = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TarifaLa__0924F76CBAC003F7", x => x.IdTarifaLavanderia);
                });

            migrationBuilder.CreateTable(
                name: "TarifaPersonaAdicional",
                columns: table => new
                {
                    IdTarifaPersonaAdicional = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Precio = table.Column<decimal>(type: "money", nullable: true),
                    Observaciones = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TarifaPe__8C301A86B84D09F3", x => x.IdTarifaPersonaAdicional);
                });

            migrationBuilder.CreateTable(
                name: "TarifaVisitaDia",
                columns: table => new
                {
                    IdTarifaVisitaDia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Precio = table.Column<decimal>(type: "money", nullable: true),
                    MinimoPersonas = table.Column<int>(type: "int", nullable: true),
                    MaximoPersonas = table.Column<int>(type: "int", nullable: true),
                    Observaciones = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TarifaVi__1F619C9F90B3051B", x => x.IdTarifaVisitaDia);
                });

            migrationBuilder.CreateTable(
                name: "Temporada",
                columns: table => new
                {
                    IdTemporada = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Descripcion = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Temporad__80F418213553B873", x => x.IdTemporada);
                });

            migrationBuilder.CreateTable(
                name: "Apartamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdCiudad = table.Column<int>(type: "int", nullable: true),
                    EsHabitacion = table.Column<bool>(type: "bit", nullable: false),
                    CapacidadMaxima = table.Column<int>(type: "int", nullable: true),
                    TieneSalaComedor = table.Column<bool>(type: "bit", nullable: true),
                    TieneCocina = table.Column<bool>(type: "bit", nullable: true),
                    TieneParqueadero = table.Column<bool>(type: "bit", nullable: true),
                    CantidadHabitaciones = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Apartame__3214EC07B3C5E9FD", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Apartamen__IdCiu__4BAC3F29",
                        column: x => x.IdCiudad,
                        principalTable: "ApartamentoCiudad",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Alojamiento",
                columns: table => new
                {
                    IdAlojamiento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSede = table.Column<int>(type: "int", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NumeroHabitaciones = table.Column<int>(type: "int", nullable: true),
                    NumeroBanos = table.Column<int>(type: "int", nullable: true),
                    CapacidadMaxima = table.Column<int>(type: "int", nullable: true),
                    Sala = table.Column<bool>(type: "bit", nullable: true),
                    Cocina = table.Column<bool>(type: "bit", nullable: true),
                    Cocineta = table.Column<bool>(type: "bit", nullable: true),
                    Terraza = table.Column<bool>(type: "bit", nullable: true),
                    Comedor = table.Column<bool>(type: "bit", nullable: true),
                    Televisor = table.Column<bool>(type: "bit", nullable: true),
                    SofaCama = table.Column<bool>(type: "bit", nullable: true),
                    Nevera = table.Column<bool>(type: "bit", nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Alojamie__EF77F57275F8EE02", x => x.IdAlojamiento);
                    table.ForeignKey(
                        name: "FK__Alojamien__IdSed__5629CD9C",
                        column: x => x.IdSede,
                        principalTable: "Sede",
                        principalColumn: "IdSede");
                });

            migrationBuilder.CreateTable(
                name: "ServicioSede",
                columns: table => new
                {
                    IdServicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSede = table.Column<int>(type: "int", nullable: true),
                    Servicio = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Servicio__2DCCF9A29A0CCA8E", x => x.IdServicio);
                    table.ForeignKey(
                        name: "FK__ServicioS__IdSed__60A75C0F",
                        column: x => x.IdSede,
                        principalTable: "Sede",
                        principalColumn: "IdSede");
                });

            migrationBuilder.CreateTable(
                name: "TarifaPersonaAdicional_Temporada",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTarifaPersonaAdicional = table.Column<int>(type: "int", nullable: true),
                    IdTemporada = table.Column<int>(type: "int", nullable: true),
                    Precio = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TarifaPe__3214EC0727863DBC", x => x.Id);
                    table.ForeignKey(
                        name: "FK__TarifaPer__IdTar__70DDC3D8",
                        column: x => x.IdTarifaPersonaAdicional,
                        principalTable: "TarifaPersonaAdicional",
                        principalColumn: "IdTarifaPersonaAdicional");
                    table.ForeignKey(
                        name: "FK__TarifaPer__IdTem__71D1E811",
                        column: x => x.IdTemporada,
                        principalTable: "Temporada",
                        principalColumn: "IdTemporada");
                });

            migrationBuilder.CreateTable(
                name: "ApartamentoBanos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdApartamento = table.Column<int>(type: "int", nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: true),
                    TieneBanoPrivado = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Apartame__3214EC07A1AF1EDA", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Apartamen__IdApa__5165187F",
                        column: x => x.IdApartamento,
                        principalTable: "Apartamento",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ApartamentoCamas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdApartamento = table.Column<int>(type: "int", nullable: true),
                    TipoCama = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Apartame__3214EC07064A1CB5", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Apartamen__IdApa__4E88ABD4",
                        column: x => x.IdApartamento,
                        principalTable: "Apartamento",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TarifaApartamento",
                columns: table => new
                {
                    IdTarifa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<int>(type: "int", nullable: true),
                    IdTemporada = table.Column<int>(type: "int", nullable: true),
                    Precio = table.Column<decimal>(type: "money", nullable: true),
                    PersonasIncluidas = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TarifaAp__78F1A91DA8173760", x => x.IdTarifa);
                    table.ForeignKey(
                        name: "FK__TarifaApa__IdTem__66603565",
                        column: x => x.IdTemporada,
                        principalTable: "Temporada",
                        principalColumn: "IdTemporada");
                    table.ForeignKey(
                        name: "FK__TarifaAparta__Id__656C112C",
                        column: x => x.Id,
                        principalTable: "Apartamento",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Habitacion",
                columns: table => new
                {
                    IdHabitacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAlojamiento = table.Column<int>(type: "int", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Habitaci__8BBBF901B69ECC42", x => x.IdHabitacion);
                    table.ForeignKey(
                        name: "FK__Habitacio__IdAlo__5AEE82B9",
                        column: x => x.IdAlojamiento,
                        principalTable: "Alojamiento",
                        principalColumn: "IdAlojamiento");
                });

            migrationBuilder.CreateTable(
                name: "TarifaAlojamiento",
                columns: table => new
                {
                    IdTarifa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAlojamiento = table.Column<int>(type: "int", nullable: true),
                    IdTemporada = table.Column<int>(type: "int", nullable: true),
                    NumeroHabitacion = table.Column<int>(type: "int", nullable: true),
                    Precio = table.Column<decimal>(type: "money", nullable: true),
                    PersonasIncluidas = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TarifaAl__78F1A91D42C6EF78", x => x.IdTarifa);
                    table.ForeignKey(
                        name: "FK__TarifaAlo__IdAlo__693CA210",
                        column: x => x.IdAlojamiento,
                        principalTable: "Alojamiento",
                        principalColumn: "IdAlojamiento");
                    table.ForeignKey(
                        name: "FK__TarifaAlo__IdTem__6A30C649",
                        column: x => x.IdTemporada,
                        principalTable: "Temporada",
                        principalColumn: "IdTemporada");
                });

            migrationBuilder.CreateTable(
                name: "Cama",
                columns: table => new
                {
                    IdCama = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdHabitacion = table.Column<int>(type: "int", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cama__3B7B1B619E80F075", x => x.IdCama);
                    table.ForeignKey(
                        name: "FK__Cama__IdHabitaci__5DCAEF64",
                        column: x => x.IdHabitacion,
                        principalTable: "Habitacion",
                        principalColumn: "IdHabitacion");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alojamiento_IdSede",
                table: "Alojamiento",
                column: "IdSede");

            migrationBuilder.CreateIndex(
                name: "IX_Apartamento_IdCiudad",
                table: "Apartamento",
                column: "IdCiudad");

            migrationBuilder.CreateIndex(
                name: "IX_ApartamentoBanos_IdApartamento",
                table: "ApartamentoBanos",
                column: "IdApartamento");

            migrationBuilder.CreateIndex(
                name: "IX_ApartamentoCamas_IdApartamento",
                table: "ApartamentoCamas",
                column: "IdApartamento");

            migrationBuilder.CreateIndex(
                name: "IX_Cama_IdHabitacion",
                table: "Cama",
                column: "IdHabitacion");

            migrationBuilder.CreateIndex(
                name: "IX_Habitacion_IdAlojamiento",
                table: "Habitacion",
                column: "IdAlojamiento");

            migrationBuilder.CreateIndex(
                name: "IX_ServicioSede_IdSede",
                table: "ServicioSede",
                column: "IdSede");

            migrationBuilder.CreateIndex(
                name: "IX_TarifaAlojamiento_IdAlojamiento",
                table: "TarifaAlojamiento",
                column: "IdAlojamiento");

            migrationBuilder.CreateIndex(
                name: "IX_TarifaAlojamiento_IdTemporada",
                table: "TarifaAlojamiento",
                column: "IdTemporada");

            migrationBuilder.CreateIndex(
                name: "IX_TarifaApartamento_Id",
                table: "TarifaApartamento",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TarifaApartamento_IdTemporada",
                table: "TarifaApartamento",
                column: "IdTemporada");

            migrationBuilder.CreateIndex(
                name: "IX_TarifaPersonaAdicional_Temporada_IdTarifaPersonaAdicional",
                table: "TarifaPersonaAdicional_Temporada",
                column: "IdTarifaPersonaAdicional");

            migrationBuilder.CreateIndex(
                name: "IX_TarifaPersonaAdicional_Temporada_IdTemporada",
                table: "TarifaPersonaAdicional_Temporada",
                column: "IdTemporada");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlojamientosCapacidadTotal");

            migrationBuilder.DropTable(
                name: "ApartamentoBanos");

            migrationBuilder.DropTable(
                name: "ApartamentoCamas");

            migrationBuilder.DropTable(
                name: "Cama");

            migrationBuilder.DropTable(
                name: "ServicioSede");

            migrationBuilder.DropTable(
                name: "TarifaAlojamiento");

            migrationBuilder.DropTable(
                name: "TarifaApartamento");

            migrationBuilder.DropTable(
                name: "TarifaLavanderia");

            migrationBuilder.DropTable(
                name: "TarifaPersonaAdicional_Temporada");

            migrationBuilder.DropTable(
                name: "TarifaVisitaDia");

            migrationBuilder.DropTable(
                name: "Habitacion");

            migrationBuilder.DropTable(
                name: "Apartamento");

            migrationBuilder.DropTable(
                name: "TarifaPersonaAdicional");

            migrationBuilder.DropTable(
                name: "Temporada");

            migrationBuilder.DropTable(
                name: "Alojamiento");

            migrationBuilder.DropTable(
                name: "ApartamentoCiudad");

            migrationBuilder.DropTable(
                name: "Sede");
        }
    }
}
