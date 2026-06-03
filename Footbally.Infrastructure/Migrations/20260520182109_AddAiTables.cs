using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Footbally.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAiTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.CreateTable(
                name: "AiJobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TargetEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TargetEntityType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, defaultValue: "Pending"),
                    InputJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutputJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConfidenceScore = table.Column<float>(type: "real", nullable: true),
                    AdminReviewRequired = table.Column<bool>(type: "bit", nullable: false),
                    AdminReviewed = table.Column<bool>(type: "bit", nullable: false),
                    ReviewedByAdminId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AdminNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TokensUsed = table.Column<int>(type: "int", nullable: false),
                    ModelUsed = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AiJobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AiModerationResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AiJobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TargetEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TargetEntityType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RiskLevel = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "low"),
                    FlagsJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminReviewRequired = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AiModerationResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AiModerationResults_AiJobs_AiJobId",
                        column: x => x.AiJobId,
                        principalTable: "AiJobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AiPlayerRatings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AiJobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OverallRating = table.Column<int>(type: "int", nullable: false),
                    Pace = table.Column<int>(type: "int", nullable: false),
                    Shooting = table.Column<int>(type: "int", nullable: false),
                    Passing = table.Column<int>(type: "int", nullable: false),
                    Defending = table.Column<int>(type: "int", nullable: false),
                    Physical = table.Column<int>(type: "int", nullable: false),
                    Technique = table.Column<int>(type: "int", nullable: false),
                    GameIntelligence = table.Column<int>(type: "int", nullable: false),
                    CardTier = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ConfidenceScore = table.Column<float>(type: "real", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AiPlayerRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AiPlayerRatings_AiJobs_AiJobId",
                        column: x => x.AiJobId,
                        principalTable: "AiJobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AiScoutReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScoutId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AiJobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Verdict = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ConfidenceScore = table.Column<float>(type: "real", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AiScoutReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AiScoutReports_AiJobs_AiJobId",
                        column: x => x.AiJobId,
                        principalTable: "AiJobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AiTrustScores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AiJobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Score = table.Column<float>(type: "real", nullable: false),
                    ScoreLabel = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SignalsJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConfidenceScore = table.Column<float>(type: "real", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AiTrustScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AiTrustScores_AiJobs_AiJobId",
                        column: x => x.AiJobId,
                        principalTable: "AiJobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AiJobs_JobType",
                table: "AiJobs",
                column: "JobType");

            migrationBuilder.CreateIndex(
                name: "IX_AiJobs_Status",
                table: "AiJobs",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_AiJobs_TargetEntityId",
                table: "AiJobs",
                column: "TargetEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_AiModerationResults_AiJobId",
                table: "AiModerationResults",
                column: "AiJobId");

            migrationBuilder.CreateIndex(
                name: "IX_AiModerationResults_TargetEntityId",
                table: "AiModerationResults",
                column: "TargetEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_AiPlayerRatings_AiJobId",
                table: "AiPlayerRatings",
                column: "AiJobId");

            migrationBuilder.CreateIndex(
                name: "IX_AiPlayerRatings_PlayerId",
                table: "AiPlayerRatings",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_AiScoutReports_AiJobId",
                table: "AiScoutReports",
                column: "AiJobId");

            migrationBuilder.CreateIndex(
                name: "IX_AiScoutReports_PlayerId",
                table: "AiScoutReports",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_AiScoutReports_ScoutId",
                table: "AiScoutReports",
                column: "ScoutId");

            migrationBuilder.CreateIndex(
                name: "IX_AiTrustScores_AiJobId",
                table: "AiTrustScores",
                column: "AiJobId");

            migrationBuilder.CreateIndex(
                name: "IX_AiTrustScores_PlayerId",
                table: "AiTrustScores",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AiModerationResults");

            migrationBuilder.DropTable(
                name: "AiPlayerRatings");

            migrationBuilder.DropTable(
                name: "AiScoutReports");

            migrationBuilder.DropTable(
                name: "AiTrustScores");

            migrationBuilder.DropTable(
                name: "AiJobs");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
