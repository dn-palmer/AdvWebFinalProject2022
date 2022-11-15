using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnDAPI.Migrations
{
    public partial class Mig01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    DungeonAndDragons1E = table.Column<bool>(type: "bit", nullable: true),
                    AdvancedDnD1E = table.Column<bool>(type: "bit", nullable: true),
                    AdvancedDnD2E = table.Column<bool>(type: "bit", nullable: true),
                    DungeonAndDragons3E = table.Column<bool>(type: "bit", nullable: true),
                    DungeonAndDragons4E = table.Column<bool>(type: "bit", nullable: true),
                    DungeonAndDragons5E = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DungeonMasters", x => x.Id);
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
                    DungeonAndDragons1E = table.Column<bool>(type: "bit", nullable: true),
                    AdvancedDnD1E = table.Column<bool>(type: "bit", nullable: true),
                    AdvancedDnD2E = table.Column<bool>(type: "bit", nullable: true),
                    DungeonAndDragons3E = table.Column<bool>(type: "bit", nullable: true),
                    DungeonAndDragons4E = table.Column<bool>(type: "bit", nullable: true),
                    DungeonAndDragons5E = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Campaigns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameEdition = table.Column<int>(type: "int", nullable: false),
                    CampaignName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CampaignDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DungeonMasterId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Campaigns_DungeonMasters_DungeonMasterId",
                        column: x => x.DungeonMasterId,
                        principalTable: "DungeonMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Campaigns_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_DungeonMasterId",
                table: "Campaigns",
                column: "DungeonMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_PlayerId",
                table: "Campaigns",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Campaigns");

            migrationBuilder.DropTable(
                name: "DungeonMasters");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
