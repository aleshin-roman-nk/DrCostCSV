using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RequireBudgetLine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                UPDATE ExpenseDocumentItems
                SET BudgetLineId = (
                    SELECT BudgetLineId
                    FROM BudgetTags
                    WHERE BudgetTags.Id = ExpenseDocumentItems.BudgetTagId)
                WHERE BudgetLineId IS NULL
                AND BudgetTagId IS NOT NULL;
                """);

            migrationBuilder.Sql(
                """
                INSERT INTO BudgetLines (Name)
                SELECT 'Требует распределения'
                WHERE EXISTS (
                    SELECT 1
                    FROM ExpenseDocumentItems
                    WHERE BudgetLineId IS NULL)
                AND NOT EXISTS (
                    SELECT 1
                    FROM BudgetLines
                    WHERE Name = 'Требует распределения');
                """);

            migrationBuilder.Sql(
                """
                UPDATE ExpenseDocumentItems
                SET BudgetLineId = (
                    SELECT Id
                    FROM BudgetLines
                    WHERE Name = 'Требует распределения')
                WHERE BudgetLineId IS NULL;
                """);

            migrationBuilder.Sql(
                """
                UPDATE ExpenseDocumentItems
                SET BudgetTagId = NULL
                WHERE BudgetTagId IS NOT NULL
                AND NOT EXISTS (
                    SELECT 1
                    FROM BudgetTags
                    WHERE BudgetTags.Id = ExpenseDocumentItems.BudgetTagId
                    AND BudgetTags.BudgetLineId = ExpenseDocumentItems.BudgetLineId);
                """);

            migrationBuilder.AlterColumn<int>(
                name: "BudgetLineId",
                table: "ExpenseDocumentItems",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BudgetLineId",
                table: "ExpenseDocumentItems",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}
