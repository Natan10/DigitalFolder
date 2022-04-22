using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalFolder.Migrations
{
    public partial class changeFilePathtoFileinTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "Transactions",
                newName: "File");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "File",
                table: "Transactions",
                newName: "FilePath");
        }
    }
}
