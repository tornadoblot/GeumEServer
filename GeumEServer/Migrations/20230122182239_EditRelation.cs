using Microsoft.EntityFrameworkCore.Migrations;

namespace GeumEServer.Migrations
{
    public partial class EditRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dogs_Walks_WalkId",
                table: "Dogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Places_Walks_WalkId",
                table: "Places");

            migrationBuilder.RenameColumn(
                name: "WalkId",
                table: "Places",
                newName: "walkId");

            migrationBuilder.RenameIndex(
                name: "IX_Places_WalkId",
                table: "Places",
                newName: "IX_Places_walkId");

            migrationBuilder.RenameColumn(
                name: "WalkId",
                table: "Dogs",
                newName: "walkId");

            migrationBuilder.RenameIndex(
                name: "IX_Dogs_WalkId",
                table: "Dogs",
                newName: "IX_Dogs_walkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dogs_Walks_walkId",
                table: "Dogs",
                column: "walkId",
                principalTable: "Walks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Places_Walks_walkId",
                table: "Places",
                column: "walkId",
                principalTable: "Walks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dogs_Walks_walkId",
                table: "Dogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Places_Walks_walkId",
                table: "Places");

            migrationBuilder.RenameColumn(
                name: "walkId",
                table: "Places",
                newName: "WalkId");

            migrationBuilder.RenameIndex(
                name: "IX_Places_walkId",
                table: "Places",
                newName: "IX_Places_WalkId");

            migrationBuilder.RenameColumn(
                name: "walkId",
                table: "Dogs",
                newName: "WalkId");

            migrationBuilder.RenameIndex(
                name: "IX_Dogs_walkId",
                table: "Dogs",
                newName: "IX_Dogs_WalkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dogs_Walks_WalkId",
                table: "Dogs",
                column: "WalkId",
                principalTable: "Walks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Places_Walks_WalkId",
                table: "Places",
                column: "WalkId",
                principalTable: "Walks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
