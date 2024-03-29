﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace GentWebApi.Migrations
{
    public partial class UsesCoupon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Promotions_Coupon_CouponId",
                table: "Promotions");

            migrationBuilder.DropTable(
                name: "Coupon");

            migrationBuilder.DropIndex(
                name: "IX_Promotions_CouponId",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "CouponId",
                table: "Promotions");

            migrationBuilder.AddColumn<bool>(
                name: "UsesCoupon",
                table: "Promotions",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsesCoupon",
                table: "Promotions");

            migrationBuilder.AddColumn<string>(
                name: "CouponId",
                table: "Promotions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Coupon",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupon", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Promotions_CouponId",
                table: "Promotions",
                column: "CouponId");

            migrationBuilder.AddForeignKey(
                name: "FK_Promotions_Coupon_CouponId",
                table: "Promotions",
                column: "CouponId",
                principalTable: "Coupon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
