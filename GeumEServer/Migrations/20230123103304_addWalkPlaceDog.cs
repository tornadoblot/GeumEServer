using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GeumEServer.Migrations
{
    public partial class addWalkPlaceDog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WalkDogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    walkId = table.Column<int>(nullable: true),
                    dogId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalkDogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WalkDogs_Dogs_dogId",
                        column: x => x.dogId,
                        principalTable: "Dogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WalkDogs_Walks_walkId",
                        column: x => x.walkId,
                        principalTable: "Walks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WalkPlaces",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    walkId = table.Column<int>(nullable: true),
                    placeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalkPlaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WalkPlaces_Places_placeId",
                        column: x => x.placeId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WalkPlaces_Walks_walkId",
                        column: x => x.walkId,
                        principalTable: "Walks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WalkDogs_dogId",
                table: "WalkDogs",
                column: "dogId");

            migrationBuilder.CreateIndex(
                name: "IX_WalkDogs_walkId",
                table: "WalkDogs",
                column: "walkId");

            migrationBuilder.CreateIndex(
                name: "IX_WalkPlaces_placeId",
                table: "WalkPlaces",
                column: "placeId");

            migrationBuilder.CreateIndex(
                name: "IX_WalkPlaces_walkId",
                table: "WalkPlaces",
                column: "walkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WalkDogs");

            migrationBuilder.DropTable(
                name: "WalkPlaces");
        }
    }
}
