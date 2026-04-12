using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Domain.UserAggregate;

namespace UserManagement.Infrastructure.Persistence.Configurations;

public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder
            .Property(u => u.Id)
            .HasColumnType("uuid")
            .ValueGeneratedNever();

        builder.Property(u => u.Name);
        builder.Property(u => u.Email);
        builder.Property(u => u.Age);
    }
}