using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwimCheck.API.Migrations.SwimCheckAuthDb
{
    /// <inheritdoc />
    public partial class SeedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "223e9e53-2097-4272-b7de-5045d3696bef", "223e9e53-2097-4272-b7de-5045d3696bef", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3c2fe5d4-1217-49b9-bd96-8ad2106be03e", 0, "f83064dd-4562-49cd-b34d-018c8936654f", "admin@swimcheck.local", true, false, null, "ADMIN@SWIMCHECK.LOCAL", "ADMIN@SWIMCHECK.LOCAL", "AQAAAAIAAYagAAAAEPzwo9Bcc/QeCYAWPLJqIxwoKz/TrKWI1XHJSUdiUs/pG4DpvCAVqowMmUvWfs1WPA==", null, false, "ec846e41-a108-4d92-a96b-1496aa2dbe14", false, "admin@swimcheck.local" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "223e9e53-2097-4272-b7de-5045d3696bef", "3c2fe5d4-1217-49b9-bd96-8ad2106be03e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "223e9e53-2097-4272-b7de-5045d3696bef", "3c2fe5d4-1217-49b9-bd96-8ad2106be03e" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "223e9e53-2097-4272-b7de-5045d3696bef");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3c2fe5d4-1217-49b9-bd96-8ad2106be03e");
        }
    }
}
