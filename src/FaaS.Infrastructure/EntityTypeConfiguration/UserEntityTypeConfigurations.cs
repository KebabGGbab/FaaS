using FaaS.Domain.Aggregates.UserAggregate;
using FaaS.Domain.Aggregates.UserCredentialsAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FaaS.Infrastructure.EntityTypeConfiguration
{
	internal sealed class UserEntityTypeConfigurations : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("Users");

			builder.HasOne<UserCredentials>()
				.WithOne()
				.HasPrincipalKey<UserCredentials>(u => u.Id)
				.HasForeignKey<User>(u => u.CredentialsId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);

			builder.OwnsOne(p => p.Name, p =>
			{
				p.Property(x => x.Value)
					.HasColumnName("Name");
			});
		}
	}
}
