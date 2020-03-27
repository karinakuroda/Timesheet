using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Timesheet.Data.Migrations
{
    public partial class AddTimshetIdReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Timesheets_TimesheetId",
                table: "Appointments");

            migrationBuilder.AlterColumn<Guid>(
                name: "TimesheetId",
                table: "Appointments",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Timesheets_TimesheetId",
                table: "Appointments",
                column: "TimesheetId",
                principalTable: "Timesheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Timesheets_TimesheetId",
                table: "Appointments");

            migrationBuilder.AlterColumn<Guid>(
                name: "TimesheetId",
                table: "Appointments",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Timesheets_TimesheetId",
                table: "Appointments",
                column: "TimesheetId",
                principalTable: "Timesheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
