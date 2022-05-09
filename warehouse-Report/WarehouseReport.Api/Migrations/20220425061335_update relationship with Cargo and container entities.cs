using Microsoft.EntityFrameworkCore.Migrations;

namespace WarehouseReport.Api.Migrations
{
    public partial class updaterelationshipwithCargoandcontainerentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cargo_Container_ContainerModelContainerId",
                table: "Cargo");

            migrationBuilder.RenameColumn(
                name: "ContainerModelContainerId",
                table: "Cargo",
                newName: "ContainerId");

            migrationBuilder.RenameIndex(
                name: "IX_Cargo_ContainerModelContainerId",
                table: "Cargo",
                newName: "IX_Cargo_ContainerId");

            migrationBuilder.AddColumn<int>(
                name: "ContainreId",
                table: "Cargo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Cargo_Container_ContainerId",
                table: "Cargo",
                column: "ContainerId",
                principalTable: "Container",
                principalColumn: "ContainerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cargo_Container_ContainerId",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "ContainreId",
                table: "Cargo");

            migrationBuilder.RenameColumn(
                name: "ContainerId",
                table: "Cargo",
                newName: "ContainerModelContainerId");

            migrationBuilder.RenameIndex(
                name: "IX_Cargo_ContainerId",
                table: "Cargo",
                newName: "IX_Cargo_ContainerModelContainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cargo_Container_ContainerModelContainerId",
                table: "Cargo",
                column: "ContainerModelContainerId",
                principalTable: "Container",
                principalColumn: "ContainerId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
