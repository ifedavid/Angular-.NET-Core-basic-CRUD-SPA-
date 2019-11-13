using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication5.Migrations
{
    public partial class spendingModelupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Spendings",
                table: "Spendings");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Spendings");

            migrationBuilder.AddColumn<Guid>(
                name: "DateId",
                table: "Spendings",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Spendings",
                table: "Spendings",
                column: "DateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Spendings",
                table: "Spendings");

            migrationBuilder.DropColumn(
                name: "DateId",
                table: "Spendings");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Spendings",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Spendings",
                table: "Spendings",
                column: "Id");
        }
    }
}
