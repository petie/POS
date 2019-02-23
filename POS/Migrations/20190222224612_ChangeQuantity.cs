using Microsoft.EntityFrameworkCore.Migrations;

namespace POS.Migrations
{
    public partial class ChangeQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "ReceiptItems",
                type: "decimal(13, 4)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "Change",
                table: "Payments",
                type: "decimal(13, 4)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountPayed",
                table: "Payments",
                type: "decimal(13, 4)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Payments",
                type: "decimal(13, 4)",
                nullable: false,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "ReceiptItems",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(13, 4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Change",
                table: "Payments",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(13, 4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountPayed",
                table: "Payments",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(13, 4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Payments",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(13, 4)");
        }
    }
}
