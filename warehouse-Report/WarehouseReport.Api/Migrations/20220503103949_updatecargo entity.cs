using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WarehouseReport.Api.Migrations
{
    public partial class updatecargoentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
