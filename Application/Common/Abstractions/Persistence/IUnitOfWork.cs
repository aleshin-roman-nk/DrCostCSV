using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Abstractions.Persistence
{
	public interface IUnitOfWork
	{
		int SaveChanges();
	}
}
