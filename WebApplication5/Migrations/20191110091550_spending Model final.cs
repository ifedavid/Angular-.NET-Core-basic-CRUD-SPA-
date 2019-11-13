using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication5.Migrations
{
    public partial class spendingModelfinal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DailySpendingsDateId",
                table: "Categories",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_DailySpendingsDateId",
                table: "Categories",
                column: "DailySpendingsDateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Spendings_DailySpendingsDateId",
                table: "Categories",
                column: "DailySpendingsDateId",
                principalTable: "Spendings",
                principalColumn: "DateId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Spendings_DailySpendingsDateId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_DailySpendingsDateId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DailySpendingsDateId",
                table: "Categories");
        }
    }
}
