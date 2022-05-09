using Microsoft.EntityFrameworkCore.Migrations;

namespace WarehouseReport.Api.Migrations
{
    public partial class updatedEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChargeableWeight",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "KgCBMConversion",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "VolumeCbm",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "VolumeMetric",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Cargo");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Cargo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ChargeableWeight",
                table: "Cargo",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Cargo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Height",
                table: "Cargo",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "KgCBMConversion",
                table: "Cargo",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Length",
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

            migrationBuilder.AddColumn<double>(
                name: "VolumeMetric",
                table: "Cargo",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "Cargo",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Width",
                table: "Cargo",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
