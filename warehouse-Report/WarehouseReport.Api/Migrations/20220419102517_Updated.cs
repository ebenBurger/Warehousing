using Microsoft.EntityFrameworkCore.Migrations;

namespace WarehouseReport.Api.Migrations
{
    public partial class Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cargo_ContainerModel_ContainerModelContainerId",
                table: "Cargo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContainerModel",
                table: "ContainerModel");

            migrationBuilder.RenameTable(
                name: "ContainerModel",
                newName: "Container");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Container",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsComplete",
                table: "Container",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Container",
                table: "Container",
                column: "ContainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cargo_Container_ContainerModelContainerId",
                table: "Cargo",
                column: "ContainerModelContainerId",
                principalTable: "Container",
                principalColumn: "ContainerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cargo_Container_ContainerModelContainerId",
                table: "Cargo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Container",
                table: "Container");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Container");

            migrationBuilder.DropColumn(
                name: "IsComplete",
                table: "Container");

            migrationBuilder.RenameTable(
                name: "Container",
                newName: "ContainerModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContainerModel",
                table: "ContainerModel",
                column: "ContainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cargo_ContainerModel_ContainerModelContainerId",
                table: "Cargo",
                column: "ContainerModelContainerId",
                principalTable: "ContainerModel",
                principalColumn: "ContainerId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
