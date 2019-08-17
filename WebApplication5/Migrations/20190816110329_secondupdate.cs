using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication5.Migrations
{
    public partial class secondupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Tours",
                newName: "TourId");

            migrationBuilder.RenameColumn(
                name: "Fullname",
                table: "TourPlanners",
                newName: "TourPlannerPhoneNumber");

            migrationBuilder.RenameColumn(
                name: "About",
                table: "TourPlanners",
                newName: "TourPlannerPassword");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TourPlanners",
                newName: "TourPlannerId");

            migrationBuilder.AddColumn<string>(
                name: "AboutTourPlanner",
                table: "TourPlanners",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TourPlannerEmail",
                table: "TourPlanners",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TourPlannerFullname",
                table: "TourPlanners",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TourPlannerWebsite",
                table: "TourPlanners",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3", null, "planner", "PLANNER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DropColumn(
                name: "AboutTourPlanner",
                table: "TourPlanners");

            migrationBuilder.DropColumn(
                name: "TourPlannerEmail",
                table: "TourPlanners");

            migrationBuilder.DropColumn(
                name: "TourPlannerFullname",
                table: "TourPlanners");

            migrationBuilder.DropColumn(
                name: "TourPlannerWebsite",
                table: "TourPlanners");

            migrationBuilder.RenameColumn(
                name: "TourId",
                table: "Tours",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "TourPlannerPhoneNumber",
                table: "TourPlanners",
                newName: "Fullname");

            migrationBuilder.RenameColumn(
                name: "TourPlannerPassword",
                table: "TourPlanners",
                newName: "About");

            migrationBuilder.RenameColumn(
                name: "TourPlannerId",
                table: "TourPlanners",
                newName: "Id");
        }
    }
}
