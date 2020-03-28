using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Timesheet.Data.Migrations
{
    public partial class addModelTimesheet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Timesheets",
                columns: new[] { "Id", "UserName" },
                values: new object[] { new Guid("0dbce2c5-daad-475b-aec7-406987548287"), "karina.kuroda" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Timesheets",
                keyColumn: "Id",
                keyValue: new Guid("0dbce2c5-daad-475b-aec7-406987548287"));
        }
    }
}
