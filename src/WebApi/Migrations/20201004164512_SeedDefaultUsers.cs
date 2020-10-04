using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCoreAxampleAuth.Migrations
{
    public partial class SeedDefaultUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e7a2fe5d-eefd-4a6f-ba9c-bb60c497d441");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ff17740e-23ab-4674-95bd-41f972288dd8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7b5e8e61-093d-497f-8408-61162d399e8d", "baa095eb-381d-4d3c-a545-73a3806fe0ba", "Manager", "MANAGER" },
                    { "3e489281-9523-44cc-8a56-01a77644aa52", "37d34571-5475-40a2-aadc-7dc5364c304c", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "fb859dc0-1e55-4cae-821b-9e3e863757b4", 0, "4e5e92e2-9d00-459d-b9fe-701c8b6e7109", "Admin@Admin.com", true, "Admin", "Admin", false, null, "ADMIN@ADMIN.COM", "ADMIN", "AQAAAAEAACcQAAAAEKRLTsFVnHEkVlQiaK2oXlpp0hW0PxW8xmCFFH8nnmyn56A8IJYS8LeoSmunC/ZSjA==", "XXXXXXXXXXXXX", true, "00000000-0000-0000-0000-000000000000", false, "admin" },
                    { "5441637f-6290-4925-8afa-dab9254ea8a8", 0, "e33c1ac4-0987-4779-9d77-3bdb19d01a5c", "Manager@Manager.com", true, "Manager", "Manager", false, null, "MANAGER@MANAGER.COM", "MANAGER", "AQAAAAEAACcQAAAAEKiLuMaVEXV770fQzBxs+H9jrxjMnOS4n8p8LQQDQacruICtEoTec/fihQFaIidY9g==", "XXXXXXXXXXXXX", true, "00000000-0000-0000-0000-000000000000", false, "manager" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "fb859dc0-1e55-4cae-821b-9e3e863757b4", "7b5e8e61-093d-497f-8408-61162d399e8d" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "5441637f-6290-4925-8afa-dab9254ea8a8", "3e489281-9523-44cc-8a56-01a77644aa52" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "5441637f-6290-4925-8afa-dab9254ea8a8", "3e489281-9523-44cc-8a56-01a77644aa52" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "fb859dc0-1e55-4cae-821b-9e3e863757b4", "7b5e8e61-093d-497f-8408-61162d399e8d" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3e489281-9523-44cc-8a56-01a77644aa52");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7b5e8e61-093d-497f-8408-61162d399e8d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5441637f-6290-4925-8afa-dab9254ea8a8");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fb859dc0-1e55-4cae-821b-9e3e863757b4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e7a2fe5d-eefd-4a6f-ba9c-bb60c497d441", "1b12a803-1f78-48db-9538-df40c0f82538", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ff17740e-23ab-4674-95bd-41f972288dd8", "34fc1ebd-b4ff-4203-acf0-7f7ea94917a7", "Administrator", "ADMINISTRATOR" });
        }
    }
}
