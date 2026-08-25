using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityConfiguration;

public sealed class BudgetTagConfiguration : IEntityTypeConfiguration<BudgetTag>
{
	public void Configure(EntityTypeBuilder<BudgetTag> entity)
	{
		entity.ToTable("BudgetTags");
		entity.HasKey(x => x.Id);
		entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
		entity.HasIndex(x => new { x.BudgetLineId, x.Name }).IsUnique();
		entity.HasOne(x => x.BudgetLine).WithMany().HasForeignKey(x => x.BudgetLineId).OnDelete(DeleteBehavior.Cascade);
	}
}
