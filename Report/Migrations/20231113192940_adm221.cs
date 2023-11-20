using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Report.Migrations
{
    public partial class adm221 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Rent_Buy",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Rent_Buy");
        }
    }
}
