using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estimate.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Teste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "ProductInEstimate",
                newName: "Price_UnitPrice");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "ProductInEstimate",
                newName: "Price_Quantity");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price_UnitPrice",
                table: "ProductInEstimate",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price_UnitPrice",
                table: "ProductInEstimate",
                newName: "UnitPrice");

            migrationBuilder.RenameColumn(
                name: "Price_Quantity",
                table: "ProductInEstimate",
                newName: "Quantity");

            migrationBuilder.AlterColumn<double>(
                name: "UnitPrice",
                table: "ProductInEstimate",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);
        }
    }
}
