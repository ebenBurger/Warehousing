using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WarehouseReport.Api.Migrations
{
    public partial class addedCargoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cargo",
                columns: table => new
                {
                    CargoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Supplier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCollected = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Length = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    CargoReadyPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDateOfFreeStorage = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InvoieNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KgCBMConvertion = table.Column<double>(type: "float", nullable: false),
                    Volume = table.Column<double>(type: "float", nullable: false),
                    ChargeableWeigth = table.Column<double>(type: "float", nullable: false),
                    NumberOfStorageDays = table.Column<int>(type: "int", nullable: false),
                    StorageCost = table.Column<double>(type: "float", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargo", x => x.CargoId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cargo");
        }
    }
}
