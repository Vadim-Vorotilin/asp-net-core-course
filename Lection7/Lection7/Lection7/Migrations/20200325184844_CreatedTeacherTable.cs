using Microsoft.EntityFrameworkCore.Migrations;

namespace Lection7.Migrations
{
    public partial class CreatedTeacherTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TeacherName",
                table: "Students",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    Discipline = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.Name);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Teacher_TeacherName",
                table: "Students");

            migrationBuilder.DropTable(
                name: "Teacher");

            migrationBuilder.DropIndex(
                name: "IX_Students_TeacherName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "TeacherName",
                table: "Students");
        }
    }
}
