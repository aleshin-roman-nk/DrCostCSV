using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.EntityConfiguration;

public sealed class ExpenseDocumentItemConfiguration
	: IEntityTypeConfiguration<ExpenseDocumentItem>
{
	public void Configure(EntityTypeBuilder<ExpenseDocumentItem> entity)
	{
		entity.HasKey(x => x.Id);

		entity.Property(x => x.Name)
			.HasMaxLength(300)
			.IsRequired();

		entity.Property(x => x.Price)
			.HasColumnType("decimal(18,2)");

		entity.Property(x => x.Amount)
			.HasColumnType("decimal(18,3)");

		entity.Ignore(x => x.Sum);

		entity.HasOne(x => x.BudgetLine)
			.WithMany()
			.HasForeignKey(x => x.BudgetLineId)
			.IsRequired()
			.OnDelete(DeleteBehavior.Restrict);

		entity.HasOne(x => x.BudgetTag)
			.WithMany()
			.HasForeignKey(x => x.BudgetTagId)
			.IsRequired(false)
			.OnDelete(DeleteBehavior.SetNull);
	}
}
