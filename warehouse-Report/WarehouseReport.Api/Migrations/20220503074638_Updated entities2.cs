using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WarehouseReport.Api.Migrations
{
    public partial class Updatedentities2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateComplete",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "DateOfCollection",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "DeliveryArea",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "SpecialRequirements",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "Transporter",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "TransporterCost",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "TransporterInvoiceDate",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "TransporterInvoiceNumber",
                table: "Cargo");

            migrationBuilder.AddColumn<int>(
                name: "TransporterId",
                table: "Package",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Transporter",
                columns: table => new
                {
                    TransporterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeliveryArea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfCollection = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TransporterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransporterCost = table.Column<double>(type: "float", nullable: true),
                    TransporterInvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransporterInvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SpecialRequirements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateComplete = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transporter", x => x.TransporterId);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Package_Transporter_TransporterId",
                table: "Package");

            migrationBuilder.DropTable(
                name: "Transporter");

            migrationBuilder.DropIndex(
                name: "IX_Package_TransporterId",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "TransporterId",
                table: "Package");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateComplete",
                table: "Cargo",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfCollection",
                table: "Cargo",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryArea",
                table: "Cargo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecialRequirements",
                table: "Cargo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Transporter",
                table: "Cargo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TransporterCost",
                table: "Cargo",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TransporterInvoiceDate",
                table: "Cargo",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransporterInvoiceNumber",
                table: "Cargo",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
