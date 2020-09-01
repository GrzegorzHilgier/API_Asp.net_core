using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace REST_API.Migrations.Cart
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cart");

            migrationBuilder.CreateTable(
                name: "CartUser",
                schema: "cart",
                columns: table => new
                {
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartUser", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "CartSession",
                schema: "cart",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    ValidityDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartSession", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartSession_CartUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "cart",
                        principalTable: "CartUser",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                schema: "cart",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    CartSessionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_CartSession_CartSessionId",
                        column: x => x.CartSessionId,
                        principalSchema: "cart",
                        principalTable: "CartSession",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartSession_UserId",
                schema: "cart",
                table: "CartSession",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CartSessionId",
                schema: "cart",
                table: "Items",
                column: "CartSessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items",
                schema: "cart");

            migrationBuilder.DropTable(
                name: "CartSession",
                schema: "cart");

            migrationBuilder.DropTable(
                name: "CartUser",
                schema: "cart");
        }
    }
}
