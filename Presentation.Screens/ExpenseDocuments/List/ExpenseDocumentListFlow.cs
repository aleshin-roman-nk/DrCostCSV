using Microsoft.Extensions.DependencyInjection;
using Presentation.Screens.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Presentation.Screens.ExpenseDocuments.List;

public class ExpenseDocumentListFlow
{
	private readonly IScreenScopedExecutor screenScopedExecutor;

	public ExpenseDocumentListFlow(IScreenScopedExecutor screenScopedExecutor)
	{
		this.screenScopedExecutor = screenScopedExecutor;
	}

	public void Run(DateTime dt)
	{
		screenScopedExecutor.Execute<ExpenseDocumentListPresenter, ScreenResult>(p => p.Run(dt));
	}
}
