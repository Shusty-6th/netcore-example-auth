using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCoreAxampleAuth.Migrations
{
    public partial class InitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Companies",
                table: "Companies");

            migrationBuilder.RenameTable(
                name: "Companies",
                newName: "Products");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "ProductId");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Color", "IsGoodQuality", "Name" },
                values: new object[] { 1, 2, true, "Bike" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Color", "IsGoodQuality", "Name" },
                values: new object[] { 2, 1, false, "Jogging pants" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Color", "IsGoodQuality", "Name" },
                values: new object[] { 3, 3, true, "Ball" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Companies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Companies",
                table: "Companies",
                column: "ProductId");
        }
    }
}
