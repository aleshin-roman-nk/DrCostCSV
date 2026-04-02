using DrCostCSV.Screens.ExpenseDocuments.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DrCostCSV.Screens.ExpenseDocuments.Edit.Parsing
{
	public class DocumentItemsJsonParser
	{
		public IEnumerable<ExpenseDocumentItemRowViewModel> Parse(string jsonSrc)
		{
			if (string.IsNullOrWhiteSpace(jsonSrc))
				return [];

			try
			{
				var items = JsonSerializer.Deserialize<List<ExpenseDocumentItemRowViewModel>>(
					jsonSrc,
					new JsonSerializerOptions
					{
						PropertyNameCaseInsensitive = true
					});

				return items ?? [];
			}
			catch (JsonException ex)
			{
				throw new InvalidOperationException("Invalid JSON.", ex);
			}
		}
	}
}
