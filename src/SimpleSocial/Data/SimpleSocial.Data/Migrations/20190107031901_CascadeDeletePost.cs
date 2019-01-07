using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleSocial.Data.Migrations
{
    public partial class CascadeDeletePost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostReports_Posts_PostId",
                table: "PostReports");

            migrationBuilder.AddForeignKey(
                name: "FK_PostReports_Posts_PostId",
                table: "PostReports",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostReports_Posts_PostId",
                table: "PostReports");

            migrationBuilder.AddForeignKey(
                name: "FK_PostReports_Posts_PostId",
                table: "PostReports",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
