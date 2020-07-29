using Microsoft.EntityFrameworkCore.Migrations;

namespace REST_API.Migrations
{
    public partial class Added_IsInactive_field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "catalog",
                table: "Items",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "catalog",
                table: "Items");
        }
    }
}
