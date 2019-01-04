using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleSocial.Data.Migrations
{
    public partial class ReportReasons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReportReason",
                table: "PostReports",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReportReason",
                table: "PostReports");
        }
    }
}
