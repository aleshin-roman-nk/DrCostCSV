using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrencyDefaults : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurrencyDefaultSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    DocumentCurrencyId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyDefaultSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrencyDefaultSettings_Currencies_DocumentCurrencyId",
                        column: x => x.DocumentCurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseDocumentCurrencyValues",
                columns: table => new
                {
                    CurrencyId = table.Column<int>(type: "INTEGER", nullable: false),
                    ExpenseDocumentId = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseDocumentCurrencyValues", x => new { x.ExpenseDocumentId, x.CurrencyId });
                    table.ForeignKey(
                        name: "FK_ExpenseDocumentCurrencyValues_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExpenseDocumentCurrencyValues_ExpenseDocuments_ExpenseDocumentId",
                        column: x => x.ExpenseDocumentId,
                        principalTable: "ExpenseDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DefaultCurrencyValues",
                columns: table => new
                {
                    CurrencyId = table.Column<int>(type: "INTEGER", nullable: false),
                    SettingsId = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultCurrencyValues", x => new { x.SettingsId, x.CurrencyId });
                    table.ForeignKey(
                        name: "FK_DefaultCurrencyValues_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DefaultCurrencyValues_CurrencyDefaultSettings_SettingsId",
                        column: x => x.SettingsId,
                        principalTable: "CurrencyDefaultSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyDefaultSettings_DocumentCurrencyId",
                table: "CurrencyDefaultSettings",
                column: "DocumentCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_DefaultCurrencyValues_CurrencyId",
                table: "DefaultCurrencyValues",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseDocumentCurrencyValues_CurrencyId",
                table: "ExpenseDocumentCurrencyValues",
                column: "CurrencyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefaultCurrencyValues");

            migrationBuilder.DropTable(
                name: "ExpenseDocumentCurrencyValues");

            migrationBuilder.DropTable(
                name: "CurrencyDefaultSettings");
        }
    }
}
