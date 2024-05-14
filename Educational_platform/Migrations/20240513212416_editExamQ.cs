using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Educational_platform.Migrations
{
    /// <inheritdoc />
    public partial class editExamQ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_studentVMs_grades_GradeId",
                table: "studentVMs");

            migrationBuilder.DropIndex(
                name: "IX_studentVMs_GradeId",
                table: "studentVMs");

            migrationBuilder.AddColumn<string>(
                name: "QuestionImg",
                table: "exams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "BookVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    GradeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookVM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GradeVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeVM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LectureVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GradeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LectureVM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLoginVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RememberMe = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLoginVM", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookVM");

            migrationBuilder.DropTable(
                name: "GradeVM");

            migrationBuilder.DropTable(
                name: "LectureVM");

            migrationBuilder.DropTable(
                name: "UserLoginVM");

            migrationBuilder.DropColumn(
                name: "QuestionImg",
                table: "exams");

            migrationBuilder.CreateIndex(
                name: "IX_studentVMs_GradeId",
                table: "studentVMs",
                column: "GradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_studentVMs_grades_GradeId",
                table: "studentVMs",
                column: "GradeId",
                principalTable: "grades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
