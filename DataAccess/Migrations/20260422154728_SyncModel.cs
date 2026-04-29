using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class SyncModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscountAgreement_CustomerCompany_CompanyId",
                table: "DiscountAgreement");

            migrationBuilder.DropIndex(
                name: "IX_DiscountAgreement_CompanyId",
                table: "DiscountAgreement");
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerCompany_DiscountAgreement_DiscountAgreementDiscount~",
                table: "CustomerCompany");

            migrationBuilder.DropIndex(
                name: "IX_CustomerCompany_DiscountAgreementDiscountId",
                table: "CustomerCompany");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DiscountAgreementDiscountId",
                table: "CustomerCompany");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountAgreement_CompanyId",
                table: "DiscountAgreement",
                column: "CompanyId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountAgreement_CustomerCompany_CompanyId",
                table: "DiscountAgreement",
                column: "CompanyId",
                principalTable: "CustomerCompany",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
