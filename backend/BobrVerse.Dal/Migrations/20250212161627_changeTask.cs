using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BobrVerse.Dal.Migrations
{
    /// <inheritdoc />
    public partial class changeTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Length",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "Weigth",
                table: "Resources");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Resources",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxCollectCalls",
                table: "QuizTasks",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "MaxCollectCalls",
                table: "QuizTasks");

            migrationBuilder.AddColumn<int>(
                name: "Length",
                table: "Resources",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Weigth",
                table: "Resources",
                type: "int",
                nullable: true);
        }
    }
}
