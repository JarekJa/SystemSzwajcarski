using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemSzwajcarski.Migrations
{
    public partial class AddNumberPlayers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberPlayers",
                table: "Tournaments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberPlayers",
                table: "Tournaments");
        }
    }
}
