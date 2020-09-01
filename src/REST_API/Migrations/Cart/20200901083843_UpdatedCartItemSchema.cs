using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace REST_API.Migrations.Cart
{
    public partial class UpdatedCartItemSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_CartSession_CartSessionId",
                schema: "cart",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                schema: "cart",
                table: "Items");

            migrationBuilder.RenameTable(
                name: "Items",
                schema: "cart",
                newName: "CartItems",
                newSchema: "cart");

            migrationBuilder.RenameIndex(
                name: "IX_Items_CartSessionId",
                schema: "cart",
                table: "CartItems",
                newName: "IX_CartItems_CartSessionId");

            migrationBuilder.AddColumn<Guid>(
                name: "ItemId",
                schema: "cart",
                table: "CartItems",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItems",
                schema: "cart",
                table: "CartItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_CartSession_CartSessionId",
                schema: "cart",
                table: "CartItems",
                column: "CartSessionId",
                principalSchema: "cart",
                principalTable: "CartSession",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_CartSession_CartSessionId",
                schema: "cart",
                table: "CartItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItems",
                schema: "cart",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "ItemId",
                schema: "cart",
                table: "CartItems");

            migrationBuilder.RenameTable(
                name: "CartItems",
                schema: "cart",
                newName: "Items",
                newSchema: "cart");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_CartSessionId",
                schema: "cart",
                table: "Items",
                newName: "IX_Items_CartSessionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                schema: "cart",
                table: "Items",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_CartSession_CartSessionId",
                schema: "cart",
                table: "Items",
                column: "CartSessionId",
                principalSchema: "cart",
                principalTable: "CartSession",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
