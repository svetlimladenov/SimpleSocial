using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleSocial.Data.Migrations
{
    public partial class reportDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ReportedOn",
                table: "PostReports",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReportedOn",
                table: "PostReports");
        }
    }
}
