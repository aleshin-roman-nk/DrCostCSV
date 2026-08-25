using Microsoft.Extensions.DependencyInjection;
using Presentation.Screens.Common;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace Presentation.Screens.ExpenseDocuments.Edit;

public class ExpenseDocumentEditFlow
{
	private readonly IScreenScopedExecutor screenScopedExecutor;

	public ExpenseDocumentEditFlow(IScreenScopedExecutor screenScopedExecutor)
	{
		this.screenScopedExecutor = screenScopedExecutor;
	}

	public ScreenResult RunCreate()
	{
		return RunCreate(DateTime.Today);
	}

	public ScreenResult RunCreate(DateTime date)
	{
		return screenScopedExecutor.Execute<ExpenseDocumentEditPresenter, ScreenResult>(p => p.CreateDocument(date));
	}

	public ScreenResult RunEdit(int id)
	{
		return screenScopedExecutor.Execute<ExpenseDocumentEditPresenter, ScreenResult>(p => p.OpenDocument(id));
	}
}
