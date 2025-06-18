using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mission.Entities.Migrations
{
    /// <inheritdoc />
    public partial class amendmissionfields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MissionAvailability",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "MissionDocuments",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "MissionOrganisationDetail",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "MissionOrganisationName",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "MissionType",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "MissionVideoUrl",
                table: "Missions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MissionAvailability",
                table: "Missions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MissionDocuments",
                table: "Missions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MissionOrganisationDetail",
                table: "Missions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MissionOrganisationName",
                table: "Missions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MissionType",
                table: "Missions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MissionVideoUrl",
                table: "Missions",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
