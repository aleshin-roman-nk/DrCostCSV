using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations;

public partial class RenameCategoriesToBudgetLinesAndAddBudgetTags : Migration
{
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropForeignKey("FK_ExpenseDocumentItems_Categories_CategoryId", "ExpenseDocumentItems");
		migrationBuilder.RenameTable(name: "Categories", newName: "BudgetLines");
		migrationBuilder.RenameColumn(name: "CategoryId", table: "ExpenseDocumentItems", newName: "BudgetLineId");
		migrationBuilder.RenameIndex(name: "IX_ExpenseDocumentItems_CategoryId", table: "ExpenseDocumentItems", newName: "IX_ExpenseDocumentItems_BudgetLineId");

		migrationBuilder.CreateIndex(name: "IX_BudgetLines_Name", table: "BudgetLines", column: "Name", unique: true);

		migrationBuilder.CreateTable(
			name: "BudgetTags",
			columns: table => new
			{
				Id = table.Column<int>(type: "INTEGER", nullable: false).Annotation("Sqlite:Autoincrement", true),
				BudgetLineId = table.Column<int>(type: "INTEGER", nullable: false),
				Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_BudgetTags", x => x.Id);
				table.ForeignKey("FK_BudgetTags_BudgetLines_BudgetLineId", x => x.BudgetLineId, "BudgetLines", "Id", onDelete: ReferentialAction.Cascade);
			});
		migrationBuilder.CreateIndex(name: "IX_BudgetTags_BudgetLineId_Name", table: "BudgetTags", columns: new[] { "BudgetLineId", "Name" }, unique: true);
		migrationBuilder.AddColumn<int>(name: "BudgetTagId", table: "ExpenseDocumentItems", type: "INTEGER", nullable: true);
		migrationBuilder.CreateIndex(name: "IX_ExpenseDocumentItems_BudgetTagId", table: "ExpenseDocumentItems", column: "BudgetTagId");
		migrationBuilder.AddForeignKey(name: "FK_ExpenseDocumentItems_BudgetLines_BudgetLineId", table: "ExpenseDocumentItems", column: "BudgetLineId", principalTable: "BudgetLines", principalColumn: "Id", onDelete: ReferentialAction.Restrict);
		migrationBuilder.AddForeignKey(name: "FK_ExpenseDocumentItems_BudgetTags_BudgetTagId", table: "ExpenseDocumentItems", column: "BudgetTagId", principalTable: "BudgetTags", principalColumn: "Id", onDelete: ReferentialAction.SetNull);
	}

	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropForeignKey("FK_ExpenseDocumentItems_BudgetLines_BudgetLineId", "ExpenseDocumentItems");
		migrationBuilder.DropForeignKey("FK_ExpenseDocumentItems_BudgetTags_BudgetTagId", "ExpenseDocumentItems");
		migrationBuilder.DropTable("BudgetTags");
		migrationBuilder.DropIndex("IX_BudgetLines_Name", "BudgetLines");
		migrationBuilder.DropIndex("IX_ExpenseDocumentItems_BudgetTagId", "ExpenseDocumentItems");
		migrationBuilder.DropColumn("BudgetTagId", "ExpenseDocumentItems");
		migrationBuilder.RenameColumn(name: "BudgetLineId", table: "ExpenseDocumentItems", newName: "CategoryId");
		migrationBuilder.RenameIndex(name: "IX_ExpenseDocumentItems_BudgetLineId", table: "ExpenseDocumentItems", newName: "IX_ExpenseDocumentItems_CategoryId");
		migrationBuilder.RenameTable(name: "BudgetLines", newName: "Categories");
		migrationBuilder.AddForeignKey(name: "FK_ExpenseDocumentItems_Categories_CategoryId", table: "ExpenseDocumentItems", column: "CategoryId", principalTable: "Categories", principalColumn: "Id", onDelete: ReferentialAction.SetNull);
	}
}
