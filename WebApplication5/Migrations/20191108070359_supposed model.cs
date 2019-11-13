using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication5.Migrations
{
    public partial class supposedmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Spendings_SpendingsDate",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Spendings",
                table: "Spendings");

            migrationBuilder.DropIndex(
                name: "IX_Categories_SpendingsDate",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "SpendingsDate",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "TimeStamp",
                table: "Categories",
                newName: "UpdatedAt");

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "Spendings",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Spendings",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Categories",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "SpendingsId",
                table: "Categories",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Spendings",
                table: "Spendings",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_SpendingsId",
                table: "Categories",
                column: "SpendingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Spendings_SpendingsId",
                table: "Categories",
                column: "SpendingsId",
                principalTable: "Spendings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Spendings_SpendingsId",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Spendings",
                table: "Spendings");

            migrationBuilder.DropIndex(
                name: "IX_Categories_SpendingsId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Spendings");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "SpendingsId",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Categories",
                newName: "TimeStamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Spendings",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SpendingsDate",
                table: "Categories",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Spendings",
                table: "Spendings",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_SpendingsDate",
                table: "Categories",
                column: "SpendingsDate");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Spendings_SpendingsDate",
                table: "Categories",
                column: "SpendingsDate",
                principalTable: "Spendings",
                principalColumn: "Date",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
