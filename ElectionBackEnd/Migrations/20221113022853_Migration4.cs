using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectionBackEnd.Migrations
{
    public partial class Migration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Voted",
                table: "Electers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Voted",
                table: "Electers");
        }
    }
}
