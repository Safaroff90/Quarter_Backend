using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quarter.Migrations
{
    public partial class UpdateTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Areas_Areas_AreaId",
                table: "Areas");

            migrationBuilder.DropIndex(
                name: "IX_Areas_AreaId",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Areas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "Areas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Areas_AreaId",
                table: "Areas",
                column: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Areas_Areas_AreaId",
                table: "Areas",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id");
        }
    }
}
