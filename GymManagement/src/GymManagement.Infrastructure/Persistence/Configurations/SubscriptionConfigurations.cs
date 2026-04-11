using GymManagement.Domain.SubscriptionAggregate;
using GymManagement.Infrastructure.Persistence.Convertors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagement.Infrastructure.Persistence.Configurations;

public class SubscriptionConfigurations: IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder
            .Property("_gymIds")
            .HasColumnName("GymIds")
            .HasConversion<ListOfIdsConverter>();
            
        builder
            .Property("_maxGyms")
            .HasColumnName("MaxGyms")
            .HasColumnType("int");
        
        builder
            .Property("_adminId")
            .HasColumnName("AdminId")
            .HasColumnType("uuid");

        builder
            .Property(s => s.Id)
            .HasColumnType("uuid")
            .ValueGeneratedNever();

        builder.HasKey(s => s.Id);

        builder.Property(s => s.SubscriptionType);
    }
}