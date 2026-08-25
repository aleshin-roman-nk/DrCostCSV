using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfiguration;

public sealed class BudgetLineConfiguration : IEntityTypeConfiguration<BudgetLine>
{
	public void Configure(EntityTypeBuilder<BudgetLine> entity)
	{
		entity.ToTable("BudgetLines");
		entity.HasKey(x => x.Id);
		entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
		entity.HasIndex(x => x.Name).IsUnique();
	}
}
