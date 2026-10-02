using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfiguration;

public sealed class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
	public void Configure(EntityTypeBuilder<Currency> entity)
	{
		entity.HasKey(currency => currency.Id);
		entity.Property(currency => currency.Code).HasMaxLength(3).UseCollation("NOCASE").IsRequired();
		entity.HasIndex(currency => currency.Code).IsUnique();
		entity.Property(currency => currency.Name).HasMaxLength(100).IsRequired();
	}
}
