using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication5.Migrations
{
    public partial class spendingModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Spendings_SpendingsId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_SpendingsId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "SpendingsId",
                table: "Categories");

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Spendings",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "SpendingsId",
                table: "Categories",
                nullable: true);

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
    }
}
