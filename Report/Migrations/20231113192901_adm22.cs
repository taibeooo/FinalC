using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Report.Migrations
{
    public partial class adm22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RentBuyPrice",
                table: "Rent_Buy",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "RentBuyArea",
                table: "Rent_Buy",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "RentBuyImage1",
                table: "Rent_Buy",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RentBuyImage2",
                table: "Rent_Buy",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentBuyImage1",
                table: "Rent_Buy");

            migrationBuilder.DropColumn(
                name: "RentBuyImage2",
                table: "Rent_Buy");

            migrationBuilder.AlterColumn<string>(
                name: "RentBuyPrice",
                table: "Rent_Buy",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "RentBuyArea",
                table: "Rent_Buy",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
