using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Shopping_Cart.Migrations
{
    /// <inheritdoc />
    public partial class Category : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Catogories_BrandId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Catogories_CategoryId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Catogories",
                table: "Catogories");

            migrationBuilder.RenameTable(
                name: "Catogories",
                newName: "Categories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_BrandId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Catogories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Catogories",
                table: "Catogories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Catogories_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Catogories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Catogories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Catogories",
                principalColumn: "Id");
        }
    }
}
