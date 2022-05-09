using Microsoft.EntityFrameworkCore.Migrations;

namespace WarehouseReport.Api.Migrations
{
    public partial class updatedpackageandcontainerrelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContainerId",
                table: "Package",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Package_ContainerId",
                table: "Package",
                column: "ContainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Package_Container_ContainerId",
                table: "Package",
                column: "ContainerId",
                principalTable: "Container",
                principalColumn: "ContainerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Package_Container_ContainerId",
                table: "Package");

            migrationBuilder.DropIndex(
                name: "IX_Package_ContainerId",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "ContainerId",
                table: "Package");
        }
    }
}
