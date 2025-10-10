using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwimCheck.API.Migrations
{
    /// <inheritdoc />
    public partial class EnrollDeleteRestrict : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrolls_Athletes_AthleteId",
                table: "Enrolls");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrolls_Races_RaceId",
                table: "Enrolls");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrolls_Athletes_AthleteId",
                table: "Enrolls",
                column: "AthleteId",
                principalTable: "Athletes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrolls_Races_RaceId",
                table: "Enrolls",
                column: "RaceId",
                principalTable: "Races",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrolls_Athletes_AthleteId",
                table: "Enrolls");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrolls_Races_RaceId",
                table: "Enrolls");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrolls_Athletes_AthleteId",
                table: "Enrolls",
                column: "AthleteId",
                principalTable: "Athletes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrolls_Races_RaceId",
                table: "Enrolls",
                column: "RaceId",
                principalTable: "Races",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
