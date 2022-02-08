using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopApplication.DataLayer.Migrations
{
    public partial class Init_factor_factorDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Factor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PayDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PayTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PayNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsPayed = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Factor_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FactorDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FactorId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductCount = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactorDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FactorDetail_Factor_FactorId",
                        column: x => x.FactorId,
                        principalTable: "Factor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FactorDetail_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Factor_UserId",
                table: "Factor",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FactorDetail_FactorId",
                table: "FactorDetail",
                column: "FactorId");

            migrationBuilder.CreateIndex(
                name: "IX_FactorDetail_ProductId",
                table: "FactorDetail",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FactorDetail");

            migrationBuilder.DropTable(
                name: "Factor");
        }
    }
}
