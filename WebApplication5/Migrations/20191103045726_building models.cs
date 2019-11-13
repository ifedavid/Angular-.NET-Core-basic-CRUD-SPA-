using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication5.Migrations
{
    public partial class buildingmodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tours");

            migrationBuilder.DropTable(
                name: "TourPlanners");

            migrationBuilder.CreateTable(
                name: "Spendings",
                columns: table => new
                {
                    Date = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spendings", x => x.Date);
                    table.ForeignKey(
                        name: "FK_Spendings_UserData_UserId",
                        column: x => x.UserId,
                        principalTable: "UserData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Amount = table.Column<int>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    SpendingsDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Spendings_SpendingsDate",
                        column: x => x.SpendingsDate,
                        principalTable: "Spendings",
                        principalColumn: "Date",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_SpendingsDate",
                table: "Categories",
                column: "SpendingsDate");

            migrationBuilder.CreateIndex(
                name: "IX_Spendings_UserId",
                table: "Spendings",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Spendings");

            migrationBuilder.CreateTable(
                name: "TourPlanners",
                columns: table => new
                {
                    TourPlannerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AboutTourPlanner = table.Column<string>(nullable: true),
                    TourPlannerEmail = table.Column<string>(nullable: false),
                    TourPlannerFullname = table.Column<string>(nullable: false),
                    TourPlannerPassword = table.Column<string>(nullable: false),
                    TourPlannerPhoneNumber = table.Column<string>(nullable: false),
                    TourPlannerWebsite = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourPlanners", x => x.TourPlannerId);
                });

            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    TourId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    OptinPrice = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    TourName = table.Column<string>(nullable: false),
                    TourPlannerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.TourId);
                    table.ForeignKey(
                        name: "FK_Tours_TourPlanners_TourPlannerId",
                        column: x => x.TourPlannerId,
                        principalTable: "TourPlanners",
                        principalColumn: "TourPlannerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tours_TourPlannerId",
                table: "Tours",
                column: "TourPlannerId");
        }
    }
}
