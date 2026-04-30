using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserFavorites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_CustomerCompany_CustomerCompanyCompanyId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_CustomerCompanyCompanyId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CustomerCompanyCompanyId",
                table: "Product");

            migrationBuilder.CreateTable(
                name: "UserFavoriteProducts",
                columns: table => new
                {
                    FavoriteProductsProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    FavoritedByUsersUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavoriteProducts", x => new { x.FavoriteProductsProductId, x.FavoritedByUsersUserId });
                    table.ForeignKey(
                        name: "FK_UserFavoriteProducts_Product_FavoriteProductsProductId",
                        column: x => x.FavoriteProductsProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFavoriteProducts_User_FavoritedByUsersUserId",
                        column: x => x.FavoritedByUsersUserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFavoriteProducts_FavoritedByUsersUserId",
                table: "UserFavoriteProducts",
                column: "FavoritedByUsersUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFavoriteProducts");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerCompanyCompanyId",
                table: "Product",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_CustomerCompanyCompanyId",
                table: "Product",
                column: "CustomerCompanyCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_CustomerCompany_CustomerCompanyCompanyId",
                table: "Product",
                column: "CustomerCompanyCompanyId",
                principalTable: "CustomerCompany",
                principalColumn: "CompanyId");
        }
    }
}
