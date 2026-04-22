using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDiscountAgreementRelation : Migration
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

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "DiscountAgreementDiscountId",
                table: "CustomerCompany",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCompany_DiscountAgreementDiscountId",
                table: "CustomerCompany",
                column: "DiscountAgreementDiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerCompany_DiscountAgreement_DiscountAgreementDiscount~",
                table: "CustomerCompany",
                column: "DiscountAgreementDiscountId",
                principalTable: "DiscountAgreement",
                principalColumn: "DiscountId",
                onDelete: ReferentialAction.Cascade);
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
