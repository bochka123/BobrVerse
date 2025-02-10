using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BobrVerse.Dal.Migrations
{
    /// <inheritdoc />
    public partial class ChangeQuizTaskModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_QuizTasks_CollectResourcesTaskId",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Resources");

            migrationBuilder.RenameColumn(
                name: "CollectResourcesTaskId",
                table: "Resources",
                newName: "QuizTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Resources_CollectResourcesTaskId",
                table: "Resources",
                newName: "IX_Resources_QuizTaskId");

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "Resources",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsTemplate",
                table: "QuizTasks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "QuizTasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_QuizTasks_QuizTaskId",
                table: "Resources",
                column: "QuizTaskId",
                principalTable: "QuizTasks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_QuizTasks_QuizTaskId",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "IsTemplate",
                table: "QuizTasks");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "QuizTasks");

            migrationBuilder.RenameColumn(
                name: "QuizTaskId",
                table: "Resources",
                newName: "CollectResourcesTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Resources_QuizTaskId",
                table: "Resources",
                newName: "IX_Resources_CollectResourcesTaskId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Resources",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Resources",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_QuizTasks_CollectResourcesTaskId",
                table: "Resources",
                column: "CollectResourcesTaskId",
                principalTable: "QuizTasks",
                principalColumn: "Id");
        }
    }
}
