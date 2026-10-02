using Domain;

namespace Domain.Tests;

public sealed class ExpenseDocumentTests
{
	[Fact]
	public void AddItem_ValidItem_AddsItemAndCalculatesTotal()
	{
		// Arrange: создаём документ, над которым будет выполняться проверка.
		var document = CreateDocument();

		// Act: выполняем одно проверяемое действие.
		document.AddItem(
			name: "Молоко",
			price: 3.50m,
			amount: 2m,
			budgetLineId: 1);

		// Assert: проверяем наблюдаемый результат действия.
		var item = Assert.Single(document.Items);
		Assert.Equal("Молоко", item.Name);
		Assert.Equal(7.00m, item.Sum);
		Assert.Equal(7.00m, document.TotalSum);
	}

	[Fact]
	public void AddItem_EmptyName_ThrowsArgumentException()
	{
		// Arrange
		var document = CreateDocument();

		// Act
		var exception = Assert.Throws<ArgumentException>(() => document.AddItem(
			name: "   ",
			price: 3.50m,
			amount: 1m,
			budgetLineId: 1));

		// Assert
		Assert.Equal("name", exception.ParamName);
		Assert.Empty(document.Items);
	}

	[Fact]
	public void AddItem_NegativePrice_AddsDiscountAndReducesTotal()
	{
		var document = CreateDocument();
		document.AddItem(
			name: "Молоко",
			price: 10m,
			amount: 1m,
			budgetLineId: 1);
		document.AddItem(
			name: "Скидка",
			price: -2m,
			amount: 1m,
			budgetLineId: 1);

		Assert.Equal(-2m, document.Items[1].Sum);
		Assert.Equal(8m, document.TotalSum);
	}

	[Fact]
	public void UpdateItem_NegativePrice_ReducesTotal()
	{
		var document = CreateDocument();
		document.AddItem("Скидка", 0m, 1m, 1);

		document.UpdateItem(document.Items[0].Id, "Скидка", -3m, 1m, 1, null);

		Assert.Equal(-3m, document.TotalSum);
	}

	[Fact]
	public void AddItem_ZeroAmount_ThrowsArgumentOutOfRangeException()
	{
		// Arrange
		var document = CreateDocument();

		// Act
		var exception = Assert.Throws<ArgumentOutOfRangeException>(() => document.AddItem(
			name: "Молоко",
			price: 3.50m,
			amount: 0m,
			budgetLineId: 1));

		// Assert
		Assert.Equal("amount", exception.ParamName);
		Assert.Empty(document.Items);
	}

	[Fact]
	public void AddItem_InvalidBudgetLineId_ThrowsArgumentOutOfRangeException()
	{
		// Arrange
		var document = CreateDocument();

		// Act
		var exception = Assert.Throws<ArgumentOutOfRangeException>(() => document.AddItem(
			name: "Молоко",
			price: 3.50m,
			amount: 1m,
			budgetLineId: 0));

		// Assert
		Assert.Equal("budgetLineId", exception.ParamName);
		Assert.Empty(document.Items);
	}

	[Fact]
	public void ChangeDate_DateWithTime_StoresDateWithoutTime()
	{
		// Arrange
		var document = CreateDocument();
		var dateWithTime = new DateTime(2026, 9, 21, 18, 30, 45);

		// Act
		document.ChangeDate(dateWithTime);

		// Assert
		Assert.Equal(new DateTime(2026, 9, 21), document.Date);
		Assert.Equal(TimeSpan.Zero, document.Date.TimeOfDay);
	}

	private static ExpenseDocument CreateDocument()
	{
		return new ExpenseDocument(
			date: new DateTime(2026, 9, 20),
			sellerName: "Учебный магазин");
	}
}
