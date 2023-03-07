using Microsoft.EntityFrameworkCore.Migrations;

namespace GeumEServer.Migrations
{
    public partial class AddWalkingDogId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Walkings_Dogs_dogId",
                table: "Walkings");

            migrationBuilder.DropIndex(
                name: "IX_Walkings_dogId",
                table: "Walkings");

            migrationBuilder.AlterColumn<int>(
                name: "dogId",
                table: "Walkings",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "dogId",
                table: "Walkings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

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
    }
}
