using Microsoft.EntityFrameworkCore.Migrations;

namespace BlacksmithWorkshopDatabaseImplement.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductComponents_Products_ProductId",
                table: "ProductComponents");

            migrationBuilder.DropIndex(
                name: "IX_ProductComponents_ProductId",
                table: "ProductComponents");

            migrationBuilder.AddColumn<int>(
                name: "ManufactureId",
                table: "ProductComponents",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductComponents_ManufactureId",
                table: "ProductComponents",
                column: "ManufactureId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComponents_Products_ManufactureId",
                table: "ProductComponents",
                column: "ManufactureId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductComponents_Products_ManufactureId",
                table: "ProductComponents");

            migrationBuilder.DropIndex(
                name: "IX_ProductComponents_ManufactureId",
                table: "ProductComponents");

            migrationBuilder.DropColumn(
                name: "ManufactureId",
                table: "ProductComponents");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComponents_ProductId",
                table: "ProductComponents",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComponents_Products_ProductId",
                table: "ProductComponents",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
