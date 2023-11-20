using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Report.Migrations
{
    public partial class categorirent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Rent_Buy",
                columns: table => new
                {
                    RentBuyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RentBuyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RentBuyDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RentBuyProvince = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RentBuyPrice = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RentBuyArea = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RentBuyPhone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RentBuyImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RentBuyStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rent_Buy", x => x.RentBuyId);
                    table.ForeignKey(
                        name: "FK_Rent_Buy_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rent_Buy_CategoryId",
                table: "Rent_Buy",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rent_Buy");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
