using Application.Web.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Web.Database.Context.Configurations
{
	public sealed class CollectionConfiguration : IEntityTypeConfiguration<Collection>
	{
		public void Configure(EntityTypeBuilder<Collection> builder)
		{
			builder.ToTable("table_collection");

			// Collection - Model
			builder.HasMany(c => c.Models)
				.WithOne(m => m.Collection)
				.HasForeignKey(m => m.CollectionId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
