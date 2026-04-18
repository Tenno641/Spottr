using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Infrastructure.Outbox;

namespace UserManagement.Infrastructure.Persistence.Configurations;

public class OutboxMessageConfiguration: IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.HasKey(o => o.MessageId);
        builder.Property(o => o.MessageId)
            .HasColumnType("bigint")
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(o => o.OccurredOn);
        builder.Property(o => o.IsProcessed);
        builder.Property(o => o.Type);
        builder.Property(o => o.Body);
        builder.Property(o => o.Issuer);
    }
}