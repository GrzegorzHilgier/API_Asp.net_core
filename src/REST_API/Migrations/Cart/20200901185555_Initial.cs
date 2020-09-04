using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace REST_API.Migrations.Cart
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cart");

            migrationBuilder.CreateTable(
                name: "CartSession",
                schema: "cart",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ValidityDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartSession", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                schema: "cart",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ItemId = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    CartSessionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_CartSession_CartSessionId",
                        column: x => x.CartSessionId,
                        principalSchema: "cart",
                        principalTable: "CartSession",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartSessionId",
                schema: "cart",
                table: "CartItems",
                column: "CartSessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems",
                schema: "cart");

            migrationBuilder.DropTable(
                name: "CartSession",
                schema: "cart");
        }
    }
}
