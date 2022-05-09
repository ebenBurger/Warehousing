using Microsoft.EntityFrameworkCore.Migrations;

namespace WarehouseReport.Api.Migrations
{
    public partial class TableTweak : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Package_Cargo_CargoModelCargoId",
                table: "Package");

            migrationBuilder.DropIndex(
                name: "IX_Package_CargoModelCargoId",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "CargoModelCargoId",
                table: "Package");

            migrationBuilder.CreateIndex(
                name: "IX_Package_CargoId",
                table: "Package",
                column: "CargoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Package_Cargo_CargoId",
                table: "Package",
                column: "CargoId",
                principalTable: "Cargo",
                principalColumn: "CargoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Package_Cargo_CargoId",
                table: "Package");

            migrationBuilder.DropIndex(
                name: "IX_Package_CargoId",
                table: "Package");

            migrationBuilder.AddColumn<int>(
                name: "CargoModelCargoId",
                table: "Package",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Package_CargoModelCargoId",
                table: "Package",
                column: "CargoModelCargoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Package_Cargo_CargoModelCargoId",
                table: "Package",
                column: "CargoModelCargoId",
                principalTable: "Cargo",
                principalColumn: "CargoId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
