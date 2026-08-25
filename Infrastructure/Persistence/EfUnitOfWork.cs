using Application.Common.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence
{
	public class EfUnitOfWork : IUnitOfWork
	{
		private readonly AppDbContext dbContext;
		private readonly ILogger<EfUnitOfWork> logger;

		public EfUnitOfWork(
			AppDbContext dbContext,
			ILogger<EfUnitOfWork> logger)
		{
			this.dbContext = dbContext;
			this.logger = logger;
		}

		public int SaveChanges()
		{
			//var entries = dbContext.ChangeTracker
			//	.Entries()
			//	.Where(x => x.State != EntityState.Unchanged)
			//	.Select(x => new
			//	{
			//		Entity = x.Entity.GetType().Name,
			//		State = x.State.ToString()
			//	})
			//	.ToList();

			//foreach (var entry in entries)
			//{
			//	logger.LogInformation(
			//		"Tracked before SaveChanges: {Entity} {State}",
			//		entry.Entity,
			//		entry.State);
			//}

			return dbContext.SaveChanges();
		}
	}
}
