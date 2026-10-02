using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReportCurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReportCurrencyId",
                table: "CurrencyDefaultSettings",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyDefaultSettings_ReportCurrencyId",
                table: "CurrencyDefaultSettings",
                column: "ReportCurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrencyDefaultSettings_Currencies_ReportCurrencyId",
                table: "CurrencyDefaultSettings",
                column: "ReportCurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrencyDefaultSettings_Currencies_ReportCurrencyId",
                table: "CurrencyDefaultSettings");

            migrationBuilder.DropIndex(
                name: "IX_CurrencyDefaultSettings_ReportCurrencyId",
                table: "CurrencyDefaultSettings");

            migrationBuilder.DropColumn(
                name: "ReportCurrencyId",
                table: "CurrencyDefaultSettings");
        }
    }
}
