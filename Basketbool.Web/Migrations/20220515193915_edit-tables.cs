using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Basketbool.Web.Migrations
{
    public partial class edittables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Tournaments_SeasonId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchDayDetails_Groups_MatchDayId",
                table: "MatchDayDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Groups_MatchDayId",
                table: "Matches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tournaments",
                table: "Tournaments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Groups",
                table: "Groups");

            migrationBuilder.RenameTable(
                name: "Tournaments",
                newName: "Seasons");

            migrationBuilder.RenameTable(
                name: "Groups",
                newName: "MatchDays");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_SeasonId",
                table: "MatchDays",
                newName: "IX_MatchDays_SeasonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seasons",
                table: "Seasons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MatchDays",
                table: "MatchDays",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchDayDetails_MatchDays_MatchDayId",
                table: "MatchDayDetails",
                column: "MatchDayId",
                principalTable: "MatchDays",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchDays_Seasons_SeasonId",
                table: "MatchDays",
                column: "SeasonId",
                principalTable: "Seasons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_MatchDays_MatchDayId",
                table: "Matches",
                column: "MatchDayId",
                principalTable: "MatchDays",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchDayDetails_MatchDays_MatchDayId",
                table: "MatchDayDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchDays_Seasons_SeasonId",
                table: "MatchDays");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_MatchDays_MatchDayId",
                table: "Matches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seasons",
                table: "Seasons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MatchDays",
                table: "MatchDays");

            migrationBuilder.RenameTable(
                name: "Seasons",
                newName: "Tournaments");

            migrationBuilder.RenameTable(
                name: "MatchDays",
                newName: "Groups");

            migrationBuilder.RenameIndex(
                name: "IX_MatchDays_SeasonId",
                table: "Groups",
                newName: "IX_Groups_SeasonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tournaments",
                table: "Tournaments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Groups",
                table: "Groups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Tournaments_SeasonId",
                table: "Groups",
                column: "SeasonId",
                principalTable: "Tournaments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchDayDetails_Groups_MatchDayId",
                table: "MatchDayDetails",
                column: "MatchDayId",
                principalTable: "Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Groups_MatchDayId",
                table: "Matches",
                column: "MatchDayId",
                principalTable: "Groups",
                principalColumn: "Id");
        }
    }
}
