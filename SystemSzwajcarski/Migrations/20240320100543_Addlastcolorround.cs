using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemSzwajcarski.Migrations
{
    public partial class Addlastcolorround : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ColorLastGame",
                table: "RelationTP",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorLastGame",
                table: "RelationTP");
        }
    }
}
