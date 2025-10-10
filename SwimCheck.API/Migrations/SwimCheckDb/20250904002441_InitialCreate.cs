using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SwimCheck.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Athletes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Club = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Athletes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Races",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Stroke = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DistanceMeters = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Races", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enrolls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AthleteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RaceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnrollDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrolls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrolls_Athletes_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "Athletes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrolls_Races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Races",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Athletes",
                columns: new[] { "Id", "BirthDate", "Club", "Name" },
                values: new object[,]
                {
                    { new Guid("b1a12fe5-c033-44e1-abfc-2dc9dcf7579f"), new DateTime(1998, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lousada XXI", "Eduardo Duarte" },
                    { new Guid("e44359e5-41d3-4ac2-a346-235f8e822ff2"), new DateTime(1998, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "FC Porto", "Michael Phelps" },
                    { new Guid("e6f2bac7-19ef-4085-8857-439eb1ffec5b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(5), "SL Benfica", "Caeleb Dressel" }
                });

            migrationBuilder.InsertData(
                table: "Races",
                columns: new[] { "Id", "DistanceMeters", "Stroke" },
                values: new object[,]
                {
                    { new Guid("073d53ee-5b7c-43ad-9cc8-879e15790bbd"), 50, "Breaststroke" },
                    { new Guid("0b2e4c57-00e2-4592-b2f5-ecc3098eba8c"), 200, "Medley" },
                    { new Guid("5088c3f6-1058-449d-9fe5-70815af42061"), 400, "Freestyle" },
                    { new Guid("ed9dca4b-4dbf-4379-96fa-f077e1a9af2a"), 100, "Backstroke" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrolls_AthleteId_RaceId",
                table: "Enrolls",
                columns: new[] { "AthleteId", "RaceId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enrolls_RaceId",
                table: "Enrolls",
                column: "RaceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enrolls");

            migrationBuilder.DropTable(
                name: "Athletes");

            migrationBuilder.DropTable(
                name: "Races");
        }
    }
}
