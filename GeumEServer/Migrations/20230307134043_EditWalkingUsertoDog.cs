using Microsoft.EntityFrameworkCore.Migrations;

namespace GeumEServer.Migrations
{
    public partial class EditWalkingUsertoDog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Walkings_Users_UserId",
                table: "Walkings");

            migrationBuilder.DropIndex(
                name: "IX_Walkings_UserId",
                table: "Walkings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Walkings");

            migrationBuilder.AddColumn<int>(
                name: "dogId",
                table: "Walkings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Walkings_dogId",
                table: "Walkings",
                column: "dogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Walkings_Dogs_dogId",
                table: "Walkings",
                column: "dogId",
                principalTable: "Dogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Walkings_Dogs_dogId",
                table: "Walkings");

            migrationBuilder.DropIndex(
                name: "IX_Walkings_dogId",
                table: "Walkings");

            migrationBuilder.DropColumn(
                name: "dogId",
                table: "Walkings");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Walkings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Walkings_UserId",
                table: "Walkings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Walkings_Users_UserId",
                table: "Walkings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
