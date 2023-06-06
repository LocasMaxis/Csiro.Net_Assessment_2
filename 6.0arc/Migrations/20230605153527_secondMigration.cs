using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _6._0arc.Migrations
{
    /// <inheritdoc />
    public partial class secondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_universities",
                table: "universities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_courses",
                table: "courses");

            migrationBuilder.RenameTable(
                name: "universities",
                newName: "Universities");

            migrationBuilder.RenameTable(
                name: "courses",
                newName: "Courses");

            migrationBuilder.RenameColumn(
                name: "uniRank",
                table: "Universities",
                newName: "UniRank");

            migrationBuilder.RenameColumn(
                name: "uniName",
                table: "Universities",
                newName: "UniName");

            migrationBuilder.RenameColumn(
                name: "uniID",
                table: "Universities",
                newName: "UniID");

            migrationBuilder.RenameColumn(
                name: "courseName",
                table: "Courses",
                newName: "CourseName");

            migrationBuilder.RenameColumn(
                name: "courseID",
                table: "Courses",
                newName: "CourseID");

            migrationBuilder.AddColumn<int>(
                name: "CourseID",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CoverLetter",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Gpa",
                table: "AspNetUsers",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Resume",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UniID",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Universities",
                table: "Universities",
                column: "UniID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                table: "Courses",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CourseID",
                table: "AspNetUsers",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UniID",
                table: "AspNetUsers",
                column: "UniID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Courses_CourseID",
                table: "AspNetUsers",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "CourseID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Universities_UniID",
                table: "AspNetUsers",
                column: "UniID",
                principalTable: "Universities",
                principalColumn: "UniID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Courses_CourseID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Universities_UniID",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Universities",
                table: "Universities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CourseID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UniID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CourseID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CoverLetter",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Gpa",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Resume",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UniID",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "Universities",
                newName: "universities");

            migrationBuilder.RenameTable(
                name: "Courses",
                newName: "courses");

            migrationBuilder.RenameColumn(
                name: "UniRank",
                table: "universities",
                newName: "uniRank");

            migrationBuilder.RenameColumn(
                name: "UniName",
                table: "universities",
                newName: "uniName");

            migrationBuilder.RenameColumn(
                name: "UniID",
                table: "universities",
                newName: "uniID");

            migrationBuilder.RenameColumn(
                name: "CourseName",
                table: "courses",
                newName: "courseName");

            migrationBuilder.RenameColumn(
                name: "CourseID",
                table: "courses",
                newName: "courseID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_universities",
                table: "universities",
                column: "uniID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_courses",
                table: "courses",
                column: "courseID");
        }
    }
}
