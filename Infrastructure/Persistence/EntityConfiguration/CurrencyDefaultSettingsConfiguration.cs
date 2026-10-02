using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfiguration;

public sealed class CurrencyDefaultSettingsConfiguration : IEntityTypeConfiguration<CurrencyDefaultSettings>
{
	public void Configure(EntityTypeBuilder<CurrencyDefaultSettings> entity)
	{
		entity.ToTable("CurrencyDefaultSettings");
		entity.HasKey(settings => settings.Id);
		entity.Property(settings => settings.Id).ValueGeneratedNever();
		entity.HasOne<Currency>().WithMany().HasForeignKey(settings => settings.DocumentCurrencyId)
			.OnDelete(DeleteBehavior.Restrict);
		entity.HasOne<Currency>().WithMany().HasForeignKey(settings => settings.ReportCurrencyId)
			.OnDelete(DeleteBehavior.Restrict);
		entity.OwnsMany(settings => settings.CurrencyValues, values =>
		{
			values.ToTable("DefaultCurrencyValues");
			values.WithOwner().HasForeignKey("SettingsId");
			values.HasKey("SettingsId", nameof(CurrencyValue.CurrencyId));
			values.Property(value => value.CurrencyId).ValueGeneratedNever();
			values.Property(value => value.Value).IsRequired();
			values.HasOne<Currency>().WithMany().HasForeignKey(value => value.CurrencyId)
				.OnDelete(DeleteBehavior.Restrict);
		});
		entity.Navigation(settings => settings.CurrencyValues).UsePropertyAccessMode(PropertyAccessMode.Field);
	}
}
