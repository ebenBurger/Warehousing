using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WarehouseReport.Api.Migrations
{
    public partial class updaetpackageentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Package",
                columns: table => new
                {
                    PackageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Length = table.Column<double>(type: "float", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    KgCBMConversion = table.Column<double>(type: "float", nullable: false),
                    VolumeCbm = table.Column<double>(type: "float", nullable: false),
                    VolumeMetric = table.Column<double>(type: "float", nullable: false),
                    ChargeableWeight = table.Column<double>(type: "float", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContainerId = table.Column<int>(type: "int", nullable: false),
                    ContainerModelContainerId = table.Column<int>(type: "int", nullable: true),
                    CargoModelCargoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Package", x => x.PackageId);
                    table.ForeignKey(
                        name: "FK_Package_Cargo_CargoModelCargoId",
                        column: x => x.CargoModelCargoId,
                        principalTable: "Cargo",
                        principalColumn: "CargoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Package_Container_ContainerModelContainerId",
                        column: x => x.ContainerModelContainerId,
                        principalTable: "Container",
                        principalColumn: "ContainerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Package_CargoModelCargoId",
                table: "Package",
                column: "CargoModelCargoId");

            migrationBuilder.CreateIndex(
                name: "IX_Package_ContainerModelContainerId",
                table: "Package",
                column: "ContainerModelContainerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Package");
        }
    }
}
