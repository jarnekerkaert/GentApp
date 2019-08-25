using Microsoft.EntityFrameworkCore.Migrations;

namespace GentWebApi.Migrations
{
    public partial class UserRef : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Users_UserRef",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "UserRef",
                table: "Companies",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Companies_UserRef",
                table: "Companies",
                newName: "IX_Companies_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Users_UserId",
                table: "Companies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Users_UserId",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Companies",
                newName: "UserRef");

            migrationBuilder.RenameIndex(
                name: "IX_Companies_UserId",
                table: "Companies",
                newName: "IX_Companies_UserRef");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Users_UserRef",
                table: "Companies",
                column: "UserRef",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
