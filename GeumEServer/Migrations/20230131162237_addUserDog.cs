using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GeumEServer.Migrations
{
    public partial class addUserDog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dogs_Users_UserId",
                table: "Dogs");

            migrationBuilder.DropIndex(
                name: "IX_Dogs_UserId",
                table: "Dogs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Dogs");

            migrationBuilder.CreateTable(
                name: "UserDogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    DogId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDogs");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Dogs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dogs_UserId",
                table: "Dogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dogs_Users_UserId",
                table: "Dogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
