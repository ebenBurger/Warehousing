using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WarehouseReport.Api.Migrations
{
    public partial class UpdatedCargoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Volume",
                table: "Cargo",
                newName: "VolumeMetric");

            migrationBuilder.RenameColumn(
                name: "InvoieNumber",
                table: "Cargo",
                newName: "Transporter");

            migrationBuilder.AddColumn<bool>(
                name: "BilledToJkn",
                table: "Cargo",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CommercialInvoiceReceived",
                table: "Cargo",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfCollection",
                table: "Cargo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DeliveryArea",
                table: "Cargo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Hazardous",
                table: "Cargo",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "InvoiceNumber",
                table: "Cargo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PackingListReceived",
                table: "Cargo",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "TransportedCost",
                table: "Cargo",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "VolumeCbm",
                table: "Cargo",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BilledToJkn",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "CommercialInvoiceReceived",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "DateOfCollection",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "DeliveryArea",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "Hazardous",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "InvoiceNumber",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "PackingListReceived",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "TransportedCost",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "VolumeCbm",
                table: "Cargo");

            migrationBuilder.RenameColumn(
                name: "VolumeMetric",
                table: "Cargo",
                newName: "Volume");

            migrationBuilder.RenameColumn(
                name: "Transporter",
                table: "Cargo",
                newName: "InvoieNumber");
        }
    }
}
