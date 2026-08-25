using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfiguration;

public sealed class JsonImportPromptSettingsConfiguration : IEntityTypeConfiguration<JsonImportPromptSettings>
{
	public void Configure(EntityTypeBuilder<JsonImportPromptSettings> entity)
	{
		entity.ToTable("JsonImportPromptSettings");
		entity.HasKey(x => x.Id);
		entity.Property(x => x.AdditionalInstructions).HasMaxLength(2_000).IsRequired();
	}
}
