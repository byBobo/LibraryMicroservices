using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BorrowingsService.Migrations
{
    public partial class first_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Borrowings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idCustomer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idBook = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    from = table.Column<DateTime>(type: "datetime2", nullable: false),
                    to = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Borrowings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Borrowings");
        }
    }
}
