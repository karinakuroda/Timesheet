using Microsoft.EntityFrameworkCore.Migrations;

namespace Timesheet.Data.Migrations
{
    public partial class AddTimeSpent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Projects_ProjectId",
                table: "Appointments");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Appointments",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Projects_ProjectId",
                table: "Appointments",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Projects_ProjectId",
                table: "Appointments");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Appointments",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Projects_ProjectId",
                table: "Appointments",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
