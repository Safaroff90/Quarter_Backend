using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quarter.Migrations
{
    public partial class OrderItemAddedCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "OrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "OrderItems");
        }
    }
}
