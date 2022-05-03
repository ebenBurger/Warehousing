using Microsoft.EntityFrameworkCore.Migrations;

namespace WarehouseReport.Api.Migrations
{
    public partial class updatecontainerentiy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContainerName",
                table: "Container",
                newName: "Lading");

            migrationBuilder.AddColumn<string>(
                name: "ContainerNumber",
                table: "Container",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileReference",
                table: "Container",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContainerNumber",
                table: "Container");

            migrationBuilder.DropColumn(
                name: "FileReference",
                table: "Container");

            migrationBuilder.RenameColumn(
                name: "Lading",
                table: "Container",
                newName: "ContainerName");
        }
    }
}
