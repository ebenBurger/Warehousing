using Microsoft.EntityFrameworkCore.Migrations;

namespace WarehouseReport.Api.Migrations
{
    public partial class cargocolsupdatednames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AtraxInvoiceNumber",
                table: "Cargo",
                newName: "SupplierInvoiceNumber");

            migrationBuilder.RenameColumn(
                name: "AtraxInvoiceDate",
                table: "Cargo",
                newName: "SupplierInvoiceDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SupplierInvoiceNumber",
                table: "Cargo",
                newName: "AtraxInvoiceNumber");

            migrationBuilder.RenameColumn(
                name: "SupplierInvoiceDate",
                table: "Cargo",
                newName: "AtraxInvoiceDate");
        }
    }
}
