using FaaS.Domain.Aggregates.FunctionAggregate;
using FaaS.Domain.Aggregates.FunctionLogAggregate;
using FaaS.Domain.Aggregates.UserAggregate;
using FaaS.Domain.Aggregates.UserCredentialsAggregate;
using FaaS.Infrastructure.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;

namespace FaaS.Infrastructure
{
	public sealed class ApplicationDbContext : DbContext
	{
		public DbSet<User> Users { get; set; } = null!;

		public DbSet<UserCredentials> UserCredentials { get; set; } = null!;

		public DbSet<Function> Functions { get; set; } = null!;

		public DbSet<FunctionLog> FunctionLogs { get; set; } = null!;

		public ApplicationDbContext(DbContextOptions options) 
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new UserCredentialsEntityTypeConfiguration())
				.ApplyConfiguration(new UserEntityTypeConfigurations())
				.ApplyConfiguration(new FunctionEntityTypeConfiguration())
				.ApplyConfiguration(new FunctionLogEntityTypeConfiguration());
		}
	}
}
