using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BobrVerse.Dal.Migrations
{
    /// <inheritdoc />
    public partial class AddQuests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "BobrProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Quests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    XpForComplete = table.Column<int>(type: "int", nullable: false),
                    XpForSuccess = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TimeLimit = table.Column<TimeSpan>(type: "time", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quests_BobrProfiles_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "BobrProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "QuestResponses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    XpEarned = table.Column<int>(type: "int", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    TotalXp = table.Column<int>(type: "int", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestResponses_BobrProfiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "BobrProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestResponses_Quests_QuestId",
                        column: x => x.QuestId,
                        principalTable: "Quests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuizTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRequiredForNextStage = table.Column<bool>(type: "bit", nullable: false),
                    MaxAttempts = table.Column<int>(type: "int", nullable: true),
                    TimeLimit = table.Column<TimeSpan>(type: "time", nullable: true),
                    CodeTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizTasks_Quests_QuestId",
                        column: x => x.QuestId,
                        principalTable: "Quests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizTaskStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuizTaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestResponseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskType = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    EarnedXp = table.Column<int>(type: "int", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizTaskStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizTaskStatuses_QuestResponses_QuestResponseId",
                        column: x => x.QuestResponseId,
                        principalTable: "QuestResponses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizTaskStatuses_QuizTasks_QuizTaskId",
                        column: x => x.QuizTaskId,
                        principalTable: "QuizTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Length = table.Column<int>(type: "int", nullable: true),
                    Weigth = table.Column<int>(type: "int", nullable: true),
                    CollectResourcesTaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resources_QuizTasks_CollectResourcesTaskId",
                        column: x => x.CollectResourcesTaskId,
                        principalTable: "QuizTasks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestResponses_ProfileId",
                table: "QuestResponses",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestResponses_QuestId",
                table: "QuestResponses",
                column: "QuestId");

            migrationBuilder.CreateIndex(
                name: "IX_Quests_AuthorId",
                table: "Quests",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizTasks_QuestId",
                table: "QuizTasks",
                column: "QuestId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizTaskStatuses_QuestResponseId",
                table: "QuizTaskStatuses",
                column: "QuestResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizTaskStatuses_QuizTaskId",
                table: "QuizTaskStatuses",
                column: "QuizTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_CollectResourcesTaskId",
                table: "Resources",
                column: "CollectResourcesTaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizTaskStatuses");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "QuestResponses");

            migrationBuilder.DropTable(
                name: "QuizTasks");

            migrationBuilder.DropTable(
                name: "Quests");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "BobrProfiles");
        }
    }
}
