using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class FlatPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "FlatPrice",
                table: "Drinks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlatPrice",
                table: "Drinks");
        }
    }
}
