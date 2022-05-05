using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitalFolder.Migrations
{
    public partial class addinguniquecolumntowallet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Wallets_WalletName",
                table: "Wallets",
                column: "WalletName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Wallets_WalletName",
                table: "Wallets");
        }
    }
}
