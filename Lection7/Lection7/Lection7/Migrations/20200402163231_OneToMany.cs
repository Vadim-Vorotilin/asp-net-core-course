using Microsoft.EntityFrameworkCore.Migrations;

namespace Lection7.Migrations
{
    public partial class OneToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Teacher_TeacherName",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_TeacherName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "TeacherName",
                table: "Students");

            migrationBuilder.AddColumn<string>(
                name: "TeacherId",
                table: "Students",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_TeacherId",
                table: "Students",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Teacher_TeacherId",
                table: "Students",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Teacher_TeacherId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_TeacherId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Students");

            migrationBuilder.AddColumn<string>(
                name: "TeacherName",
                table: "Students",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_TeacherName",
                table: "Students",
                column: "TeacherName");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Teacher_TeacherName",
                table: "Students",
                column: "TeacherName",
                principalTable: "Teacher",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
