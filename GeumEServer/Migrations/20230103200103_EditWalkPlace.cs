using Microsoft.EntityFrameworkCore.Migrations;

namespace GeumEServer.Migrations
{
    public partial class EditWalkPlace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dogs_Walks_WalkId",
                table: "Dogs");

            migrationBuilder.DropIndex(
                name: "IX_Dogs_WalkId",
                table: "Dogs");

            migrationBuilder.DropColumn(
                name: "WalkId",
                table: "Dogs");

            migrationBuilder.AddColumn<int>(
                name: "WalkId",
                table: "Places",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Places_WalkId",
                table: "Places",
                column: "WalkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Places_Walks_WalkId",
                table: "Places",
                column: "WalkId",
                principalTable: "Walks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Places_Walks_WalkId",
                table: "Places");

            migrationBuilder.DropIndex(
                name: "IX_Places_WalkId",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "WalkId",
                table: "Places");

            migrationBuilder.AddColumn<int>(
                name: "WalkId",
                table: "Dogs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dogs_WalkId",
                table: "Dogs",
                column: "WalkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dogs_Walks_WalkId",
                table: "Dogs",
                column: "WalkId",
                principalTable: "Walks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
