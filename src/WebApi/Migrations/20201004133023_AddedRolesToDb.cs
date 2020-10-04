using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCoreAxampleAuth.Migrations
{
    public partial class AddedRolesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e7a2fe5d-eefd-4a6f-ba9c-bb60c497d441", "1b12a803-1f78-48db-9538-df40c0f82538", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ff17740e-23ab-4674-95bd-41f972288dd8", "34fc1ebd-b4ff-4203-acf0-7f7ea94917a7", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e7a2fe5d-eefd-4a6f-ba9c-bb60c497d441");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ff17740e-23ab-4674-95bd-41f972288dd8");
        }
    }
}
