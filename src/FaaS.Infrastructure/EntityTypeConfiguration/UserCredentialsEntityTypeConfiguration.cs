using FaaS.Domain.Aggregates.UserCredentialsAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FaaS.Infrastructure.EntityTypeConfiguration
{
	public class UserCredentialsEntityTypeConfiguration : IEntityTypeConfiguration<UserCredentials>
	{
		public void Configure(EntityTypeBuilder<UserCredentials> builder)
		{
			builder.ToTable("User_Credentials");

			builder.OwnsOne(p => p.Login, p =>
			{
				p.Property(x => x.Value)
					.HasColumnName("Login");
			});

			builder.OwnsOne(p => p.Password, p =>
			{
				p.Property(x => x.Value)
					.HasColumnName("Password");
			});
		}
	}
}
