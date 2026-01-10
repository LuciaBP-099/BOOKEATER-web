using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookEater.Migrations
{
    /// <inheritdoc />
    public partial class NewClub : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClubComment_AspNetUsers_UserId",
                table: "ClubComment");

            migrationBuilder.DropForeignKey(
                name: "FK_ClubComment_ClubPick_ClubPickId",
                table: "ClubComment");

            migrationBuilder.DropForeignKey(
                name: "FK_ClubPick_Books_BookId",
                table: "ClubPick");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClubPick",
                table: "ClubPick");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClubComment",
                table: "ClubComment");

            migrationBuilder.RenameTable(
                name: "ClubPick",
                newName: "ClubPicks");

            migrationBuilder.RenameTable(
                name: "ClubComment",
                newName: "ClubComments");

            migrationBuilder.RenameIndex(
                name: "IX_ClubPick_BookId",
                table: "ClubPicks",
                newName: "IX_ClubPicks_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_ClubComment_UserId",
                table: "ClubComments",
                newName: "IX_ClubComments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ClubComment_ClubPickId",
                table: "ClubComments",
                newName: "IX_ClubComments_ClubPickId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClubPicks",
                table: "ClubPicks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClubComments",
                table: "ClubComments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClubComments_AspNetUsers_UserId",
                table: "ClubComments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClubComments_ClubPicks_ClubPickId",
                table: "ClubComments",
                column: "ClubPickId",
                principalTable: "ClubPicks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClubPicks_Books_BookId",
                table: "ClubPicks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClubComments_AspNetUsers_UserId",
                table: "ClubComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ClubComments_ClubPicks_ClubPickId",
                table: "ClubComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ClubPicks_Books_BookId",
                table: "ClubPicks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClubPicks",
                table: "ClubPicks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClubComments",
                table: "ClubComments");

            migrationBuilder.RenameTable(
                name: "ClubPicks",
                newName: "ClubPick");

            migrationBuilder.RenameTable(
                name: "ClubComments",
                newName: "ClubComment");

            migrationBuilder.RenameIndex(
                name: "IX_ClubPicks_BookId",
                table: "ClubPick",
                newName: "IX_ClubPick_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_ClubComments_UserId",
                table: "ClubComment",
                newName: "IX_ClubComment_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ClubComments_ClubPickId",
                table: "ClubComment",
                newName: "IX_ClubComment_ClubPickId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClubPick",
                table: "ClubPick",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClubComment",
                table: "ClubComment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClubComment_AspNetUsers_UserId",
                table: "ClubComment",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClubComment_ClubPick_ClubPickId",
                table: "ClubComment",
                column: "ClubPickId",
                principalTable: "ClubPick",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClubPick_Books_BookId",
                table: "ClubPick",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
