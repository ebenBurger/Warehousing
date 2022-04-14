using Microsoft.EntityFrameworkCore.Migrations;

namespace WarehouseReport.Api.Migrations
{
    public partial class TypoFixedCon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KgCBMConvertion",
                table: "Cargo",
                newName: "KgCBMConversion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KgCBMConversion",
                table: "Cargo",
                newName: "KgCBMConvertion");
        }
    }
}
