using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Footbally.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTeamProfileFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContactEmail",
                table: "TeamProfiles",
                newName: "PreferredFormat");

            migrationBuilder.AddColumn<bool>(
                name: "IsLookingForPlayers",
                table: "TeamProfiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MatchDays",
                table: "TeamProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NeededPositions",
                table: "TeamProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLookingForPlayers",
                table: "TeamProfiles");

            migrationBuilder.DropColumn(
                name: "MatchDays",
                table: "TeamProfiles");

            migrationBuilder.DropColumn(
                name: "NeededPositions",
                table: "TeamProfiles");

            migrationBuilder.RenameColumn(
                name: "PreferredFormat",
                table: "TeamProfiles",
                newName: "ContactEmail");
        }
    }
}
