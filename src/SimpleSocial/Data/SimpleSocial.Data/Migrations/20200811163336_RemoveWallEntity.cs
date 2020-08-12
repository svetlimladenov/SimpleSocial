using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleSocial.Data.Migrations
{
    public partial class RemoveWallEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Walls_WallId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Walls_WallId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "Walls");

            migrationBuilder.DropIndex(
                name: "IX_Posts_WallId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_WallId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WallId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "WallId",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WallId",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WallId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Walls",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Walls", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_WallId",
                table: "Posts",
                column: "WallId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_WallId",
                table: "AspNetUsers",
                column: "WallId",
                unique: true,
                filter: "[WallId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Walls_WallId",
                table: "AspNetUsers",
                column: "WallId",
                principalTable: "Walls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Walls_WallId",
                table: "Posts",
                column: "WallId",
                principalTable: "Walls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
