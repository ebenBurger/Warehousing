using Microsoft.EntityFrameworkCore.Migrations;

namespace WarehouseReport.Api.Migrations
{
    public partial class updatedcontainercolname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lading",
                table: "Container",
                newName: "BillOfLading");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BillOfLading",
                table: "Container",
                newName: "Lading");
        }
    }
}
