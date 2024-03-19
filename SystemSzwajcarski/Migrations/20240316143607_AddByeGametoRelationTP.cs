using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemSzwajcarski.Migrations
{
    public partial class AddByeGametoRelationTP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Bye",
                table: "RelationTP",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bye",
                table: "RelationTP");
        }
    }
}
