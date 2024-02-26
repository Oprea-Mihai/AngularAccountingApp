using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WEB_3_Accountant.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    HourlyRate = table.Column<decimal>(type: "money", nullable: false),
                    TaxPercentage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeID);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ProjectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    InitialBudget = table.Column<int>(type: "int", nullable: false),
                    IsFinished = table.Column<bool>(type: "bit", nullable: false),
                    CurrentBudget = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ProjectID);
                });

            migrationBuilder.CreateTable(
                name: "Week",
                columns: table => new
                {
                    WeekID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: false),
                    TotalGrossAmount = table.Column<decimal>(type: "money", nullable: false),
                    TotalNetAmount = table.Column<decimal>(type: "money", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Week", x => x.WeekID);
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    TaskID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(80)", unicode: false, maxLength: 80, nullable: false),
                    Income = table.Column<decimal>(type: "money", nullable: true),
                    ProjectID = table.Column<int>(type: "int", nullable: false),
                    IsSpecial = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.TaskID);
                    table.ForeignKey(
                        name: "FK_Task_Project",
                        column: x => x.ProjectID,
                        principalTable: "Project",
                        principalColumn: "ProjectID");
                });

            migrationBuilder.CreateTable(
                name: "WeeklyCost",
                columns: table => new
                {
                    WeeklyCostID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalHours = table.Column<int>(type: "int", nullable: false),
                    GrossAmmount = table.Column<decimal>(type: "money", nullable: false),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    WeekID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeeklyCost", x => x.WeeklyCostID);
                    table.ForeignKey(
                        name: "FK_WeeklyCost_Employee",
                        column: x => x.EmployeeID,
                        principalTable: "Employee",
                        principalColumn: "EmployeeID");
                    table.ForeignKey(
                        name: "FK_WeeklyCost_Week",
                        column: x => x.WeekID,
                        principalTable: "Week",
                        principalColumn: "WeekID");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeTask",
                columns: table => new
                {
                    EmployeeTaskID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    WorkedHours = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    WeeklyCostID = table.Column<int>(type: "int", nullable: false),
                    TaskID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTask", x => x.EmployeeTaskID);
                    table.ForeignKey(
                        name: "FK_EmployeeTask_Employee",
                        column: x => x.EmployeeID,
                        principalTable: "Employee",
                        principalColumn: "EmployeeID");
                    table.ForeignKey(
                        name: "FK_EmployeeTask_Task",
                        column: x => x.TaskID,
                        principalTable: "Task",
                        principalColumn: "TaskID");
                    table.ForeignKey(
                        name: "FK_EmployeeTask_WeeklyCost",
                        column: x => x.WeeklyCostID,
                        principalTable: "WeeklyCost",
                        principalColumn: "WeeklyCostID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTask_EmployeeID",
                table: "EmployeeTask",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTask_TaskID",
                table: "EmployeeTask",
                column: "TaskID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTask_WeeklyCostID",
                table: "EmployeeTask",
                column: "WeeklyCostID");

            migrationBuilder.CreateIndex(
                name: "IX_Task_ProjectID",
                table: "Task",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_WeeklyCost_EmployeeID",
                table: "WeeklyCost",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_WeeklyCost_WeekID",
                table: "WeeklyCost",
                column: "WeekID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeTask");

            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "WeeklyCost");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Week");
        }
    }
}
