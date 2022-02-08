using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopApplication.DataLayer.Migrations
{
    public partial class Menu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    NotShow = table.Column<bool>(type: "bit", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                });

            //migrationBuilder.CreateTable(
            //    name: "Role",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        RoleName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
            //        RoleTitle = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Role", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Settings",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        KeyWords = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Telephone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
            //        Mobile = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
            //        Fax = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
            //        Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
            //        SmsServiceUserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
            //        SmsServicePassword = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
            //        SmsServiceNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
            //        FactorIsSend = table.Column<bool>(type: "bit", nullable: false),
            //        PayIsSend = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Settings", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "SocialMedia",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        SocialName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            //        SocialIcon = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        SocialLink = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
            //        NotShow = table.Column<bool>(type: "bit", nullable: false),
            //        SocialOrder = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_SocialMedia", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "State",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_State", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "User",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Mobile = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
            //        Password = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
            //        UserCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
            //        IsActive = table.Column<bool>(type: "bit", nullable: false),
            //        RefreshToken = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
            //        RoleId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_User", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_User_Role_RoleId",
            //            column: x => x.RoleId,
            //            principalTable: "Role",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "City",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
            //        StateId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_City", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_City_State_StateId",
            //            column: x => x.StateId,
            //            principalTable: "State",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Address",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        AddressText = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CityId = table.Column<int>(type: "int", nullable: false),
            //        PostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
            //        UserId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Address", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Address_City_CityId",
            //            column: x => x.CityId,
            //            principalTable: "City",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_Address_User_UserId",
            //            column: x => x.UserId,
            //            principalTable: "User",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.InsertData(
            //    table: "Role",
            //    columns: new[] { "Id", "RoleName", "RoleTitle" },
            //    values: new object[] { 1, "Admin", "مدیر" });

            //migrationBuilder.InsertData(
            //    table: "Role",
            //    columns: new[] { "Id", "RoleName", "RoleTitle" },
            //    values: new object[] { 2, "User", "کاربر" });

            //migrationBuilder.InsertData(
            //    table: "Settings",
            //    columns: new[] { "Id", "Address", "Description", "FactorIsSend", "Fax", "KeyWords", "Mobile", "Name", "PayIsSend", "SmsServiceNumber", "SmsServicePassword", "SmsServiceUserName", "Telephone" },
            //    values: new object[] { 1, null, null, false, null, null, null, "فروشگاه اینترنتی", false, null, null, null, null });

            //migrationBuilder.InsertData(
            //    table: "User",
            //    columns: new[] { "Id", "IsActive", "Mobile", "Password", "RefreshToken", "RoleId", "UserCode" },
            //    values: new object[] { 1, true, "09123456789", "OgpgJsurByZQeFnE1ZiExBrGBCvI/J9cn/fcFhZOXmM=", null, 1, "666666" });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Address_CityId",
            //    table: "Address",
            //    column: "CityId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Address_UserId",
            //    table: "Address",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_City_StateId",
            //    table: "City",
            //    column: "StateId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_User_RoleId",
            //    table: "User",
            //    column: "RoleId");
        }

        //protected override void Down(MigrationBuilder migrationBuilder)
        //{
        //    migrationBuilder.DropTable(
        //        name: "Address");

        //    migrationBuilder.DropTable(
        //        name: "Menu");

        //    migrationBuilder.DropTable(
        //        name: "Settings");

        //    migrationBuilder.DropTable(
        //        name: "SocialMedia");

        //    migrationBuilder.DropTable(
        //        name: "City");

        //    migrationBuilder.DropTable(
        //        name: "User");

        //    migrationBuilder.DropTable(
        //        name: "State");

        //    migrationBuilder.DropTable(
        //        name: "Role");
        //}
    }
}
