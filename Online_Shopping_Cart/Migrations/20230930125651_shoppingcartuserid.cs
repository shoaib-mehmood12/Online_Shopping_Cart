using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Shopping_Cart.Migrations
{
    /// <inheritdoc />
    public partial class shoppingcartuserid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_userId",
                table: "Carts");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Carts",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_userId",
                table: "Carts",
                newName: "IX_Carts_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_UserId",
                table: "Carts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_UserId",
                table: "Carts");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Carts",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                newName: "IX_Carts_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_userId",
                table: "Carts",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
