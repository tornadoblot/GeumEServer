using Microsoft.EntityFrameworkCore.Migrations;

namespace GeumEServer.Migrations
{
    public partial class Editworkplacedog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WalkDogs_Dogs_dogId",
                table: "WalkDogs");

            migrationBuilder.DropForeignKey(
                name: "FK_WalkDogs_Walks_walkId",
                table: "WalkDogs");

            migrationBuilder.DropForeignKey(
                name: "FK_WalkPlaces_Places_placeId",
                table: "WalkPlaces");

            migrationBuilder.DropForeignKey(
                name: "FK_WalkPlaces_Walks_walkId",
                table: "WalkPlaces");

            migrationBuilder.DropIndex(
                name: "IX_WalkPlaces_placeId",
                table: "WalkPlaces");

            migrationBuilder.DropIndex(
                name: "IX_WalkPlaces_walkId",
                table: "WalkPlaces");

            migrationBuilder.DropIndex(
                name: "IX_WalkDogs_dogId",
                table: "WalkDogs");

            migrationBuilder.DropIndex(
                name: "IX_WalkDogs_walkId",
                table: "WalkDogs");

            migrationBuilder.RenameColumn(
                name: "walkId",
                table: "WalkPlaces",
                newName: "WalkId");

            migrationBuilder.RenameColumn(
                name: "placeId",
                table: "WalkPlaces",
                newName: "PlaceId");

            migrationBuilder.RenameColumn(
                name: "walkId",
                table: "WalkDogs",
                newName: "WalkId");

            migrationBuilder.RenameColumn(
                name: "dogId",
                table: "WalkDogs",
                newName: "DogId");

            migrationBuilder.AlterColumn<int>(
                name: "WalkId",
                table: "WalkPlaces",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PlaceId",
                table: "WalkPlaces",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WalkId",
                table: "WalkDogs",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DogId",
                table: "WalkDogs",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WalkId",
                table: "WalkPlaces",
                newName: "walkId");

            migrationBuilder.RenameColumn(
                name: "PlaceId",
                table: "WalkPlaces",
                newName: "placeId");

            migrationBuilder.RenameColumn(
                name: "WalkId",
                table: "WalkDogs",
                newName: "walkId");

            migrationBuilder.RenameColumn(
                name: "DogId",
                table: "WalkDogs",
                newName: "dogId");

            migrationBuilder.AlterColumn<int>(
                name: "walkId",
                table: "WalkPlaces",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "placeId",
                table: "WalkPlaces",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "walkId",
                table: "WalkDogs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "dogId",
                table: "WalkDogs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_WalkPlaces_placeId",
                table: "WalkPlaces",
                column: "placeId");

            migrationBuilder.CreateIndex(
                name: "IX_WalkPlaces_walkId",
                table: "WalkPlaces",
                column: "walkId");

            migrationBuilder.CreateIndex(
                name: "IX_WalkDogs_dogId",
                table: "WalkDogs",
                column: "dogId");

            migrationBuilder.CreateIndex(
                name: "IX_WalkDogs_walkId",
                table: "WalkDogs",
                column: "walkId");

            migrationBuilder.AddForeignKey(
                name: "FK_WalkDogs_Dogs_dogId",
                table: "WalkDogs",
                column: "dogId",
                principalTable: "Dogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WalkDogs_Walks_walkId",
                table: "WalkDogs",
                column: "walkId",
                principalTable: "Walks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WalkPlaces_Places_placeId",
                table: "WalkPlaces",
                column: "placeId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WalkPlaces_Walks_walkId",
                table: "WalkPlaces",
                column: "walkId",
                principalTable: "Walks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
