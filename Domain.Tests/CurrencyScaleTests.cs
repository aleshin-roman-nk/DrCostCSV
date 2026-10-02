using Domain;

namespace Domain.Tests;

public sealed class CurrencyScaleTests
{
	[Fact]
	public void DocumentOwnsSnapshot_ChangingDefaultsDoesNotChangeIt()
	{
		var settings = new CurrencyDefaultSettings();
		settings.SetDefaults(1, [new(1, 1m), new(2, 28.5m), new(3, 0.33m)]);
		var document = new ExpenseDocument(DateTime.Today, "Магазин");
		document.SetCurrencyValues(settings.DocumentCurrencyId, settings.CurrencyValues);
		settings.SetDefaults(2, [new(1, 0.036m), new(2, 1m)]);
		Assert.Equal(1, document.CurrencyId);
		Assert.Equal(28.5m, document.CurrencyValues.Single(value => value.CurrencyId == 2).Value);
		Assert.Equal(3, document.CurrencyValues.Count);
		Assert.NotSame(settings.CurrencyValues[0], document.CurrencyValues[0]);
	}

	[Fact]
	public void InvalidScale_LeavesPreviousStateUnchanged()
	{
		var document = new ExpenseDocument(DateTime.Today, "Магазин");
		document.SetCurrencyValues(1, [new(1, 1m), new(2, 28.5m)]);
		Assert.Throws<ArgumentException>(() => document.SetCurrencyValues(1, [new(2, 1m)]));
		Assert.Throws<ArgumentException>(() => document.SetCurrencyValues(1, [new(1, 1m), new(1, 2m)]));
		Assert.Throws<ArgumentException>(() => document.ChangeCurrency(3));
		Assert.Equal(1, document.CurrencyId);
		Assert.Equal(2, document.CurrencyValues.Count);
	}

	[Fact]
	public void CurrencyValuesRequirePositiveAmounts()
	{
		Assert.Throws<ArgumentOutOfRangeException>(() => new CurrencyValue(1, 0m));
		Assert.Throws<ArgumentOutOfRangeException>(() => new CurrencyValue(1, -1m));
		Assert.Throws<ArgumentOutOfRangeException>(() => new CurrencyValue(0, 1m));
	}

	[Fact]
	public void EmptyScaleAndNonUnitReferenceAreAllowed()
	{
		var settings = new CurrencyDefaultSettings();
		settings.SetDefaults(1, []);
		Assert.Equal(1, settings.DocumentCurrencyId);
		Assert.Empty(settings.CurrencyValues);
		settings.SetDefaults(1, [new(1, 0.036m), new(2, 1m)]);
		Assert.Equal(0.036m, settings.CurrencyValues[0].Value);
		settings.SetDefaults(null, []);
		Assert.Null(settings.DocumentCurrencyId);
	}
}
