using Microsoft.EntityFrameworkCore.Migrations;

namespace WarehouseReport.Api.Migrations
{
    public partial class addedContainerTableRelationshipWithCargo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContainerId",
                table: "Cargo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ContainerModelContainerId",
                table: "Cargo",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContainerModel",
                columns: table => new
                {
                    ContainerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContainerName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContainerModel", x => x.ContainerId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cargo_ContainerModelContainerId",
                table: "Cargo",
                column: "ContainerModelContainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cargo_ContainerModel_ContainerModelContainerId",
                table: "Cargo",
                column: "ContainerModelContainerId",
                principalTable: "ContainerModel",
                principalColumn: "ContainerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cargo_ContainerModel_ContainerModelContainerId",
                table: "Cargo");

            migrationBuilder.DropTable(
                name: "ContainerModel");

            migrationBuilder.DropIndex(
                name: "IX_Cargo_ContainerModelContainerId",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "ContainerId",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "ContainerModelContainerId",
                table: "Cargo");
        }
    }
}
