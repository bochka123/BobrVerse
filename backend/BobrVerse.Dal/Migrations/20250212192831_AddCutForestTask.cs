using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BobrVerse.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddCutForestTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CutLargest",
                table: "QuizTasks",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ForestSize",
                table: "QuizTasks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TreesToCut",
                table: "QuizTasks",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CutLargest",
                table: "QuizTasks");

            migrationBuilder.DropColumn(
                name: "ForestSize",
                table: "QuizTasks");

            migrationBuilder.DropColumn(
                name: "TreesToCut",
                table: "QuizTasks");
        }
    }
}
