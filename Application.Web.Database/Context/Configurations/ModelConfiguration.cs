using Application.Web.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Web.Database.Context.Configurations
{
	public sealed class ModelConfiguration : IEntityTypeConfiguration<Model>
	{
		public void Configure(EntityTypeBuilder<Model> builder)
		{
			builder.ToTable("table_model");
		}
	}
}
