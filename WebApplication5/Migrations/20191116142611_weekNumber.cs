using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication5.Migrations
{
    public partial class weekNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Spendings_Date",
                table: "Spendings");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Spendings",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "WeekNumber",
                table: "Spendings",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeekNumber",
                table: "Spendings");

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "Spendings",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.CreateIndex(
                name: "IX_Spendings_Date",
                table: "Spendings",
                column: "Date",
                unique: true);
        }
    }
}
