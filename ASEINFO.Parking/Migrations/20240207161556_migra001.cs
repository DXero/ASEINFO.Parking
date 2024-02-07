using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ASEINFO.Parking.Migrations
{
    /// <inheritdoc />
    public partial class migra001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TiposVehiculos",
                columns: table => new
                {
                    TipoVehiculoId = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Precio = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposVehiculos", x => x.TipoVehiculoId);
                });

            migrationBuilder.InsertData(
                table: "TiposVehiculos",
                columns: new[] { "TipoVehiculoId", "Descripcion", "Precio" },
                values: new object[,]
                {
                    { 1, "NO RESIDENTES", 0.5m },
                    { 2, "RESIDENTES", 0.05m },
                    { 3, "VEHICULOS OFICIALES", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TiposVehiculos");
        }
    }
}
