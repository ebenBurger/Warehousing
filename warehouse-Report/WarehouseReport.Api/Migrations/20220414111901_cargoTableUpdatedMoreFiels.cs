using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WarehouseReport.Api.Migrations
{
    public partial class cargoTableUpdatedMoreFiels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InvoiceNumber",
                table: "Cargo",
                newName: "TransporterInvoiceNumber");

            migrationBuilder.AddColumn<DateTime>(
                name: "AtraxInvoiceDate",
                table: "Cargo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "AtraxInvoiceNumber",
                table: "Cargo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteReason",
                table: "Cargo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecialRequirements",
                table: "Cargo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TransporterInvoiceDate",
                table: "Cargo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AtraxInvoiceDate",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "AtraxInvoiceNumber",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "DeleteReason",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "SpecialRequirements",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "TransporterInvoiceDate",
                table: "Cargo");

            migrationBuilder.RenameColumn(
                name: "TransporterInvoiceNumber",
                table: "Cargo",
                newName: "InvoiceNumber");
        }
    }
}
