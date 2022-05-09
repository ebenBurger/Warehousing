using Microsoft.EntityFrameworkCore.Migrations;

namespace WarehouseReport.Api.Migrations
{
    public partial class Updatedcontainerentitycols : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FRIG20ft",
                table: "Container");

            migrationBuilder.DropColumn(
                name: "FRIG40ft",
                table: "Container");

            migrationBuilder.DropColumn(
                name: "FROOG20ft",
                table: "Container");

            migrationBuilder.DropColumn(
                name: "FROOG40ft",
                table: "Container");

            migrationBuilder.DropColumn(
                name: "GP20ft",
                table: "Container");

            migrationBuilder.DropColumn(
                name: "GP40ft",
                table: "Container");

            migrationBuilder.DropColumn(
                name: "HQ40ft",
                table: "Container");

            migrationBuilder.DropColumn(
                name: "OTIG40ft",
                table: "Container");

            migrationBuilder.DropColumn(
                name: "OTOOG20ft",
                table: "Container");

            migrationBuilder.DropColumn(
                name: "OTOOG40ft",
                table: "Container");

            migrationBuilder.DropColumn(
                name: "TOIG20ft",
                table: "Container");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FRIG20ft",
                table: "Container",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FRIG40ft",
                table: "Container",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FROOG20ft",
                table: "Container",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FROOG40ft",
                table: "Container",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "GP20ft",
                table: "Container",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "GP40ft",
                table: "Container",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HQ40ft",
                table: "Container",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OTIG40ft",
                table: "Container",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OTOOG20ft",
                table: "Container",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OTOOG40ft",
                table: "Container",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TOIG20ft",
                table: "Container",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
