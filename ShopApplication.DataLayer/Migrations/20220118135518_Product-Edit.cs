using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopApplication.DataLayer.Migrations
{
    public partial class ProductEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "NotShow",
                table: "Product",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotShow",
                table: "Product");
        }
    }
}
