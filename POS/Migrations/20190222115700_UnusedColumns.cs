using Microsoft.EntityFrameworkCore.Migrations;

namespace POS.Migrations
{
    public partial class UnusedColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ean",
                table: "ReceiptItems");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ReceiptItems");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "ReceiptItems");

            migrationBuilder.DropColumn(
                name: "TaxRate",
                table: "ReceiptItems");

            migrationBuilder.DropColumn(
                name: "TaxValue",
                table: "ReceiptItems");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "ReceiptItems");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "ReceiptItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ean",
                table: "ReceiptItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ReceiptItems",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "ReceiptItems",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TaxRate",
                table: "ReceiptItems",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TaxValue",
                table: "ReceiptItems",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "ReceiptItems",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Value",
                table: "ReceiptItems",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
