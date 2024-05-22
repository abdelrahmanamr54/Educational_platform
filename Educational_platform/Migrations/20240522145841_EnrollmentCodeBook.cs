using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Educational_platform.Migrations
{
    /// <inheritdoc />
    public partial class EnrollmentCodeBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_exams_LectureId",
                table: "exams");

            migrationBuilder.AddColumn<int>(
                name: "ExamId",
                table: "lectures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_exams_LectureId",
                table: "exams",
                column: "LectureId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_exams_LectureId",
                table: "exams");

            migrationBuilder.DropColumn(
                name: "ExamId",
                table: "lectures");

            migrationBuilder.CreateIndex(
                name: "IX_exams_LectureId",
                table: "exams",
                column: "LectureId");
        }
    }
}
