using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mission.Entities.Migrations
{
    /// <inheritdoc />
    public partial class updatenewMissionApplicationEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MissionApplications_Missions_MissionsId",
                table: "MissionApplications");

            migrationBuilder.DropIndex(
                name: "IX_MissionApplications_MissionsId",
                table: "MissionApplications");

            migrationBuilder.DropColumn(
                name: "MissionsId",
                table: "MissionApplications");

            migrationBuilder.CreateIndex(
                name: "IX_MissionApplications_MissionId",
                table: "MissionApplications",
                column: "MissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_MissionApplications_Missions_MissionId",
                table: "MissionApplications",
                column: "MissionId",
                principalTable: "Missions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MissionApplications_Missions_MissionId",
                table: "MissionApplications");

            migrationBuilder.DropIndex(
                name: "IX_MissionApplications_MissionId",
                table: "MissionApplications");

            migrationBuilder.AddColumn<int>(
                name: "MissionsId",
                table: "MissionApplications",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MissionApplications_MissionsId",
                table: "MissionApplications",
                column: "MissionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_MissionApplications_Missions_MissionsId",
                table: "MissionApplications",
                column: "MissionsId",
                principalTable: "Missions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
