using GymManagement.Domain.AdminAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagement.Infrastructure.Persistence.Configurations;

public class AdminConfigurations: IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder.Property(a => a.UserId);
        
        builder
            .Property("_subscriptionId")
            .HasColumnName("SubscriptionId")
            .HasColumnType("uuid");
        
        builder
            .Property(a => a.Id)
            .HasColumnType("uuid")
            .ValueGeneratedNever();
        
        builder
            .HasKey(s => s.Id);
    }
}