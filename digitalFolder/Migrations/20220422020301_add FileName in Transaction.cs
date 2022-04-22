using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalFolder.Migrations
{
    public partial class addFileNameinTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Transactions",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Transactions");
        }
    }
}
