using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.EntityConfiguration;

public sealed class ExpenseDocumentConfiguration
	: IEntityTypeConfiguration<ExpenseDocument>
{
	public void Configure(EntityTypeBuilder<ExpenseDocument> entity)
	{
		entity.HasKey(x => x.Id);

		entity.Property(x => x.Date)
			.IsRequired();

		entity.Property(x => x.Comment)
			.HasMaxLength(500);

		entity.HasMany(x => x.Items)
			.WithOne(x => x.ExpenseDocument)
			.HasForeignKey(x => x.ExpenseDocumentId)
			.OnDelete(DeleteBehavior.Cascade);

		entity.Navigation(x => x.Items)
			.UsePropertyAccessMode(PropertyAccessMode.Field);
	}
}
