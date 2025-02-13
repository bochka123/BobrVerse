using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BobrVerse.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrentAttempt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentAttempt",
                table: "QuizTaskStatuses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentAttempt",
                table: "QuizTaskStatuses");
        }
    }
}
