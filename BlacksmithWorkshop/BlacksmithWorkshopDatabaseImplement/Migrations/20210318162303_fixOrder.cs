using Microsoft.EntityFrameworkCore.Migrations;

namespace BlacksmithWorkshopDatabaseImplement.Migrations
{
    public partial class fixOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Products_OrderId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductComponents_Components_ComponentId",
                table: "ProductComponents");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductComponents_Products_ManufactureId",
                table: "ProductComponents");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductComponents",
                table: "ProductComponents");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductComponents");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Manufactures");

            migrationBuilder.RenameTable(
                name: "ProductComponents",
                newName: "ManufactureComponents");

            migrationBuilder.RenameColumn(
                name: "ManufacturetName",
                table: "Manufactures",
                newName: "ManufactureName");

            migrationBuilder.RenameIndex(
                name: "IX_ProductComponents_ManufactureId",
                table: "ManufactureComponents",
                newName: "IX_ManufactureComponents_ManufactureId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductComponents_ComponentId",
                table: "ManufactureComponents",
                newName: "IX_ManufactureComponents_ComponentId");

            migrationBuilder.AddColumn<int>(
                name: "ManufactureId",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ManufactureId",
                table: "ManufactureComponents",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Manufactures",
                table: "Manufactures",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ManufactureComponents",
                table: "ManufactureComponents",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ManufactureId",
                table: "Orders",
                column: "ManufactureId");

            migrationBuilder.AddForeignKey(
                name: "FK_ManufactureComponents_Components_ComponentId",
                table: "ManufactureComponents",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ManufactureComponents_Manufactures_ManufactureId",
                table: "ManufactureComponents",
                column: "ManufactureId",
                principalTable: "Manufactures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Manufactures_ManufactureId",
                table: "Orders",
                column: "ManufactureId",
                principalTable: "Manufactures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ManufactureComponents_Components_ComponentId",
                table: "ManufactureComponents");

            migrationBuilder.DropForeignKey(
                name: "FK_ManufactureComponents_Manufactures_ManufactureId",
                table: "ManufactureComponents");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Manufactures_ManufactureId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ManufactureId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Manufactures",
                table: "Manufactures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ManufactureComponents",
                table: "ManufactureComponents");

            migrationBuilder.DropColumn(
                name: "ManufactureId",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "Manufactures",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "ManufactureComponents",
                newName: "ProductComponents");

            migrationBuilder.RenameColumn(
                name: "ManufactureName",
                table: "Products",
                newName: "ManufacturetName");

            migrationBuilder.RenameIndex(
                name: "IX_ManufactureComponents_ManufactureId",
                table: "ProductComponents",
                newName: "IX_ProductComponents_ManufactureId");

            migrationBuilder.RenameIndex(
                name: "IX_ManufactureComponents_ComponentId",
                table: "ProductComponents",
                newName: "IX_ProductComponents_ComponentId");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Orders",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ManufactureId",
                table: "ProductComponents",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductComponents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductComponents",
                table: "ProductComponents",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderId",
                table: "Orders",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Products_OrderId",
                table: "Orders",
                column: "OrderId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComponents_Components_ComponentId",
                table: "ProductComponents",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComponents_Products_ManufactureId",
                table: "ProductComponents",
                column: "ManufactureId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
