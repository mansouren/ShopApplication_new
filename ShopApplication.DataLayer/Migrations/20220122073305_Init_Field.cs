using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopApplication.DataLayer.Migrations
{
    public partial class Init_Field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Feild",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feild", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductFeild",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    FeildId = table.Column<int>(type: "int", nullable: false),
                    FeildValue = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFeild", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductFeild_Feild_FeildId",
                        column: x => x.FeildId,
                        principalTable: "Feild",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductFeild_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeild_FeildId",
                table: "ProductFeild",
                column: "FeildId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeild_ProductId",
                table: "ProductFeild",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductFeild");

            migrationBuilder.DropTable(
                name: "Feild");
        }
    }
}
