using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class Flat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Flat",
                table: "BoughtDrinks",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Flat",
                table: "BoughtDrinks");
        }
    }
}
