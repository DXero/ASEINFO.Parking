using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASEINFO.Parking.Migrations
{
    /// <inheritdoc />
    public partial class migra0002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehiculos",
                columns: table => new
                {
                    VehiculoId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TipoVehiculoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculos", x => x.VehiculoId);
                    table.ForeignKey(
                        name: "FK_Vehiculos_TiposVehiculos_TipoVehiculoId",
                        column: x => x.TipoVehiculoId,
                        principalTable: "TiposVehiculos",
                        principalColumn: "TipoVehiculoId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_TipoVehiculoId",
                table: "Vehiculos",
                column: "TipoVehiculoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehiculos");
        }
    }
}
