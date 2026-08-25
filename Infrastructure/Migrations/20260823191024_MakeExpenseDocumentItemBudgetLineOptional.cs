using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeExpenseDocumentItemBudgetLineOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BudgetLineId",
                table: "ExpenseDocumentItems",
                type: "INTEGER",
                nullable: true,
				oldClrType: typeof(int),
				oldType: "INTEGER");

			migrationBuilder.Sql("UPDATE ExpenseDocumentItems SET BudgetLineId = NULL, BudgetTagId = NULL WHERE BudgetLineId = (SELECT Id FROM BudgetLines WHERE Name = 'Без строки бюджета');");
			migrationBuilder.Sql("DELETE FROM BudgetLines WHERE Name = 'Без строки бюджета';");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BudgetLineId",
                table: "ExpenseDocumentItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);
        }
    }
}
