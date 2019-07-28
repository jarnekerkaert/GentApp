using Microsoft.EntityFrameworkCore.Migrations;

namespace GentWebApi.Migrations
{
    public partial class Relational : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_BranchId",
                table: "Subscriptions",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_UserId",
                table: "Subscriptions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_BranchId",
                table: "Events",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Branches_BranchId",
                table: "Events",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Branches_BranchId",
                table: "Subscriptions",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Users_UserId",
                table: "Subscriptions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Branches_BranchId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Branches_BranchId",
                table: "Subscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Users_UserId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_BranchId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_UserId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Events_BranchId",
                table: "Events");
        }
    }
}
