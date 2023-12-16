using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Estimate.Infra.Migrations
{
    public partial class FkCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estimate_Supplier_SupplierId",
                table: "Estimate");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInEstimate_Estimate_EstimateId",
                table: "ProductInEstimate");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInEstimate_Product_ProductId",
                table: "ProductInEstimate");

            migrationBuilder.AddForeignKey(
                name: "FK_Estimate_Supplier_SupplierId",
                table: "Estimate",
                column: "SupplierId",
                principalTable: "Supplier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInEstimate_Estimate_EstimateId",
                table: "ProductInEstimate",
                column: "EstimateId",
                principalTable: "Estimate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInEstimate_Product_ProductId",
                table: "ProductInEstimate",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estimate_Supplier_SupplierId",
                table: "Estimate");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInEstimate_Estimate_EstimateId",
                table: "ProductInEstimate");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInEstimate_Product_ProductId",
                table: "ProductInEstimate");

            migrationBuilder.AddForeignKey(
                name: "FK_Estimate_Supplier_SupplierId",
                table: "Estimate",
                column: "SupplierId",
                principalTable: "Supplier",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInEstimate_Estimate_EstimateId",
                table: "ProductInEstimate",
                column: "EstimateId",
                principalTable: "Estimate",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInEstimate_Product_ProductId",
                table: "ProductInEstimate",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id");
        }
    }
}
