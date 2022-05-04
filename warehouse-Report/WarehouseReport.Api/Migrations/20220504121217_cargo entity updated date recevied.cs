using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WarehouseReport.Api.Migrations
{
    public partial class cargoentityupdateddaterecevied : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateReceived",
                table: "Cargo",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateReceived",
                table: "Cargo");
        }
    }
}
