using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASEINFO.Parking.Migrations
{
    /// <inheritdoc />
    public partial class migra0003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estancias",
                columns: table => new
                {
                    EstanciaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Entrada = table.Column<DateTime>(type: "datetime", nullable: false),
                    Salida = table.Column<DateTime>(type: "datetime", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    VehiculoId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estancias", x => x.EstanciaId);
                    table.ForeignKey(
                        name: "FK_Estancias_Vehiculos_VehiculoId",
                        column: x => x.VehiculoId,
                        principalTable: "Vehiculos",
                        principalColumn: "VehiculoId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estancias_VehiculoId",
                table: "Estancias",
                column: "VehiculoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estancias");
        }
    }
}
