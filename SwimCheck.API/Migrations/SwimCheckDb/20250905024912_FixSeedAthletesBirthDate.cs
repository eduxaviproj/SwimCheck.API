using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwimCheck.API.Migrations
{
    /// <inheritdoc />
    public partial class FixSeedAthletesBirthDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Athletes",
                keyColumn: "Id",
                keyValue: new Guid("e6f2bac7-19ef-4085-8857-439eb1ffec5b"),
                column: "BirthDate",
                value: new DateTime(1998, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Athletes",
                keyColumn: "Id",
                keyValue: new Guid("e6f2bac7-19ef-4085-8857-439eb1ffec5b"),
                column: "BirthDate",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(5));
        }
    }
}
