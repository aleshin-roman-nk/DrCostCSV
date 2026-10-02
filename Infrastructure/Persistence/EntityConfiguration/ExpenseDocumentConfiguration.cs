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

		entity.HasOne<Currency>()
			.WithMany()
			.HasForeignKey(x => x.CurrencyId)
			.OnDelete(DeleteBehavior.Restrict);

		entity.Property(x => x.Date)
			.IsRequired();

		entity.OwnsMany(document => document.CurrencyValues, values =>
		{
			values.ToTable("ExpenseDocumentCurrencyValues");
			values.WithOwner().HasForeignKey("ExpenseDocumentId");
			values.HasKey("ExpenseDocumentId", nameof(CurrencyValue.CurrencyId));
			values.Property(value => value.CurrencyId).ValueGeneratedNever();
			values.Property(value => value.Value).IsRequired();
			values.HasOne<Currency>().WithMany().HasForeignKey(value => value.CurrencyId)
				.OnDelete(DeleteBehavior.Restrict);
		});
		entity.Navigation(document => document.CurrencyValues).UsePropertyAccessMode(PropertyAccessMode.Field);

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
