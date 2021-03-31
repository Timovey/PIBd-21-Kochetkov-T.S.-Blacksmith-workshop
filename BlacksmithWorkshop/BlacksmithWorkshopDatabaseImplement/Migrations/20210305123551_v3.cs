using Microsoft.EntityFrameworkCore.Migrations;

namespace BlacksmithWorkshopDatabaseImplement.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Products_ManufactureId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ManufactureId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ManufactureId",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ManufactureId",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ManufactureId",
                table: "Orders",
                column: "ManufactureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Products_ManufactureId",
                table: "Orders",
                column: "ManufactureId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
