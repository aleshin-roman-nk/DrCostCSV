using System.ComponentModel;

namespace Presentation.Screens.CurrencyDefaults.ViewModels;

public sealed class CurrencyDefaultsViewModel
{
	public int? DocumentCurrencyId { get; set; }
	public int? ReportCurrencyId { get; set; }
	public BindingList<CurrencyValueRowViewModel> CurrencyValues { get; set; } = [];
}

public sealed class CurrencyValueRowViewModel
{
	public int CurrencyId { get; set; }
	public string Value { get; set; } = "1";
}

public sealed record CurrencyOptionViewModel(int Id, string Code);
public sealed record CurrencyDefaultsScreenModel(
	CurrencyDefaultsViewModel Settings, IReadOnlyList<CurrencyOptionViewModel> Currencies);
