using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WarehouseReport.Api.Migrations
{
    public partial class udpatedcargoentitycurrentdatetopackdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentDate",
                table: "Cargo");

            migrationBuilder.AddColumn<DateTime>(
                name: "PackDate",
                table: "Cargo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackDate",
                table: "Cargo");

            migrationBuilder.AddColumn<DateTime>(
                name: "CurrentDate",
                table: "Cargo",
                type: "datetime2",
                nullable: true);
        }
    }
}
