using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BobrVerse.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddQuestRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "QuizTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "QuestRatingId",
                table: "QuestResponses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "QuestRatings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BobrProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    QuestResponseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestRatings_BobrProfiles_BobrProfileId",
                        column: x => x.BobrProfileId,
                        principalTable: "BobrProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_QuestRatings_Quests_QuestId",
                        column: x => x.QuestId,
                        principalTable: "Quests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestResponses_QuestRatingId",
                table: "QuestResponses",
                column: "QuestRatingId",
                unique: true,
                filter: "[QuestRatingId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_QuestRatings_BobrProfileId",
                table: "QuestRatings",
                column: "BobrProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestRatings_QuestId",
                table: "QuestRatings",
                column: "QuestId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestResponses_QuestRatings_QuestRatingId",
                table: "QuestResponses",
                column: "QuestRatingId",
                principalTable: "QuestRatings",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestResponses_QuestRatings_QuestRatingId",
                table: "QuestResponses");

            migrationBuilder.DropTable(
                name: "QuestRatings");

            migrationBuilder.DropIndex(
                name: "IX_QuestResponses_QuestRatingId",
                table: "QuestResponses");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "QuizTasks");

            migrationBuilder.DropColumn(
                name: "QuestRatingId",
                table: "QuestResponses");
        }
    }
}
