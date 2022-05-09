using Microsoft.EntityFrameworkCore.Migrations;

namespace WarehouseReport.Api.Migrations
{
    public partial class packageentityupdatedcols : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Package_Container_ContainerModelContainerId",
                table: "Package");

            migrationBuilder.DropIndex(
                name: "IX_Package_ContainerModelContainerId",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "ContainerModelContainerId",
                table: "Package");

            migrationBuilder.RenameColumn(
                name: "ContainerId",
                table: "Package",
                newName: "CargoId");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Package",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Package");

            migrationBuilder.RenameColumn(
                name: "CargoId",
                table: "Package",
                newName: "ContainerId");

            migrationBuilder.AddColumn<int>(
                name: "ContainerModelContainerId",
                table: "Package",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Package_ContainerModelContainerId",
                table: "Package",
                column: "ContainerModelContainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Package_Container_ContainerModelContainerId",
                table: "Package",
                column: "ContainerModelContainerId",
                principalTable: "Container",
                principalColumn: "ContainerId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
