using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDAPI.Migrations
{
    public partial class Mig01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GamePreferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DungeonAndDragons1E = table.Column<bool>(type: "bit", nullable: false),
                    AdvancedDnD1E = table.Column<bool>(type: "bit", nullable: false),
                    AdvancedDnD2E = table.Column<bool>(type: "bit", nullable: false),
                    DungeonAndDragons3E = table.Column<bool>(type: "bit", nullable: false),
                    DungeonAndDragons4E = table.Column<bool>(type: "bit", nullable: false),
                    DungeonAndDragons5E = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePreferences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DungeonMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearsOfExperiance = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GamePreferencesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DungeonMasters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DungeonMasters_GamePreferences_GamePreferencesId",
                        column: x => x.GamePreferencesId,
                        principalTable: "GamePreferences",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearsOfExperiance = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GamePreferencesId = table.Column<int>(type: "int", nullable: true),
                    DungeonMasterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_DungeonMasters_DungeonMasterId",
                        column: x => x.DungeonMasterId,
                        principalTable: "DungeonMasters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Players_GamePreferences_GamePreferencesId",
                        column: x => x.GamePreferencesId,
                        principalTable: "GamePreferences",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DungeonMasters_GamePreferencesId",
                table: "DungeonMasters",
                column: "GamePreferencesId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_DungeonMasterId",
                table: "Players",
                column: "DungeonMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_GamePreferencesId",
                table: "Players",
                column: "GamePreferencesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "DungeonMasters");

            migrationBuilder.DropTable(
                name: "GamePreferences");
        }
    }
}
