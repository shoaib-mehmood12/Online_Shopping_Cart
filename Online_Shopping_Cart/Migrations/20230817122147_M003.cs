using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Shopping_Cart.Migrations
{
    /// <inheritdoc />
    public partial class M003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Catogories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Catogories_CategoryId1",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "Products");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Catogories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Catogories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Catogories_CategoryId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "CategoryId1",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId1",
                table: "Products",
                column: "CategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Catogories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Catogories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Catogories_CategoryId1",
                table: "Products",
                column: "CategoryId1",
                principalTable: "Catogories",
                principalColumn: "Id");
        }
    }
}
