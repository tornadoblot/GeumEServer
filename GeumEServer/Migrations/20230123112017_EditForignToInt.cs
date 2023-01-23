using Microsoft.EntityFrameworkCore.Migrations;

namespace GeumEServer.Migrations
{
    public partial class EditForignToInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Walks_Users_UserId",
                table: "Walks");

            migrationBuilder.DropIndex(
                name: "IX_Walks_UserId",
                table: "Walks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Walks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Walks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Walks_UserId",
                table: "Walks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Walks_Users_UserId",
                table: "Walks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
