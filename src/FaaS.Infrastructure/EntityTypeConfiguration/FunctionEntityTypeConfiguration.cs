using FaaS.Domain.Aggregates.FunctionAggregate;
using FaaS.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FaaS.Infrastructure.EntityTypeConfiguration
{
	internal sealed class FunctionEntityTypeConfiguration : IEntityTypeConfiguration<Function>
	{
		public void Configure(EntityTypeBuilder<Function> builder)
		{
			builder.ToTable("Functions");

			builder.HasOne<User>()
				.WithMany()
				.HasPrincipalKey(u => u.Id)
				.HasForeignKey(f => f.UserId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);

			builder.Property(f => f.Name);

			builder.Property(f => f.FileName);

			builder.Property(f => f.DirectoryName);

			builder.Property(f => f.Description);

			builder.Property(f => f.Language);

			builder.Property(f => f.CreatedAt);
		}
	}
}
