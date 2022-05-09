using Microsoft.EntityFrameworkCore.Migrations;

namespace WarehouseReport.Api.Migrations
{
    public partial class UpdatedCargoTableColNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransportedCost",
                table: "Cargo",
                newName: "TransporterCost");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransporterCost",
                table: "Cargo",
                newName: "TransportedCost");
        }
    }
}
