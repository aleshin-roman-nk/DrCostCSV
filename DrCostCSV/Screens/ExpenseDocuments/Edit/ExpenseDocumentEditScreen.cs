using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrCostCSV.Screens.ExpenseDocuments.Edit
{
	public class ExpenseDocumentEditScreen
	{
		private readonly IServiceProvider serviceProvider;

		public ExpenseDocumentEditScreen(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
		}

		public ProductSelectionResult? ShowDialog()
		{
			using var scope = _serviceProvider.CreateScope();

			var form = scope.ServiceProvider.GetRequiredService<ProductSelectionForm>();
			var presenter = ActivatorUtilities.CreateInstance<ExpenseDocumentEditPresenter>(
				scope.ServiceProvider,
				(IProductSelectionView)form);

			var result = form.ShowDialog();
			if (result != DialogResult.OK)
				return null;

			return presenter.Result;
		}
	}
}
