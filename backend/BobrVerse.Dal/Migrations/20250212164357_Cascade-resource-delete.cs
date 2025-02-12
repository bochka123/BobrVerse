using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BobrVerse.Dal.Migrations
{
    /// <inheritdoc />
    public partial class Cascaderesourcedelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_QuizTasks_QuizTaskId",
                table: "Resources");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_QuizTasks_QuizTaskId",
                table: "Resources",
                column: "QuizTaskId",
                principalTable: "QuizTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_QuizTasks_QuizTaskId",
                table: "Resources");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_QuizTasks_QuizTaskId",
                table: "Resources",
                column: "QuizTaskId",
                principalTable: "QuizTasks",
                principalColumn: "Id");
        }
    }
}
