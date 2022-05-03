using Microsoft.EntityFrameworkCore.Migrations;

namespace WarehouseReport.Api.Migrations
{
    public partial class updatetranporterrelationshiptooptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Package_Transporter_TransporterId",
                table: "Package");

            migrationBuilder.DropIndex(
                name: "IX_Package_TransporterId",
                table: "Package");

            migrationBuilder.AlterColumn<int>(
                name: "TransporterId",
                table: "Package",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Package_Transporter_TransporterId",
                table: "Package");

            migrationBuilder.DropIndex(
                name: "IX_Package_TransporterId",
                table: "Package");

            migrationBuilder.AlterColumn<int>(
                name: "TransporterId",
                table: "Package",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Package_TransporterId",
                table: "Package",
                column: "TransporterId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Package_Transporter_TransporterId",
                table: "Package",
                column: "TransporterId",
                principalTable: "Transporter",
                principalColumn: "TransporterId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
