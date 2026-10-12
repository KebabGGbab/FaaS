using FaaS.Domain.Aggregates.FunctionAggregate;
using FaaS.Domain.Aggregates.FunctionLogAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FaaS.Infrastructure.EntityTypeConfiguration
{
	internal sealed class FunctionLogEntityTypeConfiguration : IEntityTypeConfiguration<FunctionLog>
	{
		public void Configure(EntityTypeBuilder<FunctionLog> builder)
		{
			builder.ToTable("Function_Logs");

			builder.HasOne<Function>()
				.WithOne()
				.HasPrincipalKey<Function>(l => l.Id)
				.HasForeignKey<FunctionLog>(l => l.FunctionId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);

			builder.Property(l => l.ExecutedAt);

			builder.Property(l => l.ExecutionTime);

			builder.Property(l => l.IsSuccess);

			builder.Property(l => l.Result);
		}
	}
}
