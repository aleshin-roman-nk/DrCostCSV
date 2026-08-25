using Presentation.Screens.ExpenseDocuments.Edit.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Screens.ExpenseDocuments.Edit;

public static class ExpenseDocumentItemsGridSetup
{
	public static void Apply(DataGridView grid)
	{
		grid.AutoGenerateColumns = false;
		grid.Columns.Clear();

		grid.AllowUserToAddRows = false;
		grid.AllowUserToDeleteRows = false;
		grid.ReadOnly = true;
		grid.MultiSelect = false;
		grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		grid.RowHeadersVisible = false;

		AddTextColumn(grid,
			nameof(ExpenseDocumentItemViewModel.Name),
			"Наименование",
			250);

		AddDecimalColumn(grid,
			nameof(ExpenseDocumentItemViewModel.Price),
			"Цена",
			90,
			"N2");

		AddDecimalColumn(grid,
			nameof(ExpenseDocumentItemViewModel.Amount),
			"Количество",
			90,
			"N3");

		AddDecimalColumn(grid,
			nameof(ExpenseDocumentItemViewModel.Sum),
			"Сумма",
			100,
			"N2");

		AddTextColumn(grid,
			nameof(ExpenseDocumentItemViewModel.BudgetLineName),
			"Строка бюджета",
			160);

		AddTextColumn(grid,
			nameof(ExpenseDocumentItemViewModel.BudgetTagName),
			"Тег",
			140);
	}

	private static void AddTextColumn(
		DataGridView grid,
		string propertyName,
		string headerText,
		int width)
	{
		grid.Columns.Add(new DataGridViewTextBoxColumn
		{
			Name = propertyName,
			DataPropertyName = propertyName,
			HeaderText = headerText,
			Width = width,
			SortMode = DataGridViewColumnSortMode.NotSortable
		});
	}

	private static void AddDecimalColumn(
		DataGridView grid,
		string propertyName,
		string headerText,
		int width,
		string format)
	{
		grid.Columns.Add(new DataGridViewTextBoxColumn
		{
			Name = propertyName,
			DataPropertyName = propertyName,
			HeaderText = headerText,
			Width = width,
			DefaultCellStyle =
			{
				Format = format,
				Alignment = DataGridViewContentAlignment.MiddleRight
			},
			SortMode = DataGridViewColumnSortMode.NotSortable
		});
	}
}
