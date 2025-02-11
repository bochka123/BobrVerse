using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BobrVerse.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToQuestResponse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "QuestResponses");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "QuestResponses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "QuestResponses");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "QuestResponses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
