using Microsoft.EntityFrameworkCore.Migrations;

namespace WarehouseReport.Api.Migrations
{
    public partial class updatedentitiesoptionals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Package_Cargo_CargoId",
                table: "Package");

            migrationBuilder.DropForeignKey(
                name: "FK_Package_Container_ContainerId",
                table: "Package");

            migrationBuilder.AlterColumn<int>(
                name: "ContainerId",
                table: "Package",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CargoId",
                table: "Package",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Package_Cargo_CargoId",
                table: "Package",
                column: "CargoId",
                principalTable: "Cargo",
                principalColumn: "CargoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Package_Container_ContainerId",
                table: "Package",
                column: "ContainerId",
                principalTable: "Container",
                principalColumn: "ContainerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Package_Cargo_CargoId",
                table: "Package");

            migrationBuilder.DropForeignKey(
                name: "FK_Package_Container_ContainerId",
                table: "Package");

            migrationBuilder.AlterColumn<int>(
                name: "ContainerId",
                table: "Package",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CargoId",
                table: "Package",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Package_Cargo_CargoId",
                table: "Package",
                column: "CargoId",
                principalTable: "Cargo",
                principalColumn: "CargoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Package_Container_ContainerId",
                table: "Package",
                column: "ContainerId",
                principalTable: "Container",
                principalColumn: "ContainerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
