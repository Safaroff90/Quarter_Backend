using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quarter.Migrations
{
    public partial class AddedStockStatusIsHouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "StockStatus",
                table: "Houses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockStatus",
                table: "Houses");
        }
    }
}
