using Microsoft.EntityFrameworkCore.Migrations;

namespace WarehouseReport.Api.Migrations
{
    public partial class removedpackageandtransporterrelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Package_Transporter_TransporterId",
                table: "Package");

            migrationBuilder.DropIndex(
                name: "IX_Package_TransporterId",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "TransporterId",
                table: "Package");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransporterId",
                table: "Package",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Package_TransporterId",
                table: "Package",
                column: "TransporterId",
                unique: true,
                filter: "[TransporterId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Package_Transporter_TransporterId",
                table: "Package",
                column: "TransporterId",
                principalTable: "Transporter",
                principalColumn: "TransporterId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
