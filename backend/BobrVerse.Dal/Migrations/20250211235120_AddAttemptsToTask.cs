using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BobrVerse.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddAttemptsToTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeTemplate",
                table: "QuizTasks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodeTemplate",
                table: "QuizTasks",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
