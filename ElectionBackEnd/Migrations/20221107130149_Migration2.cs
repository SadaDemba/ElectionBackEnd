using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectionBackEnd.Migrations
{
    public partial class Migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Nb_ElectersAssignable",
                table: "Desks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsFull",
                table: "Centers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nb_ElectersAssignable",
                table: "Desks");

            migrationBuilder.DropColumn(
                name: "IsFull",
                table: "Centers");
        }
    }
}
