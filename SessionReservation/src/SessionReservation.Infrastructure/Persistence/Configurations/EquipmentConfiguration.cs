using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SessionReservation.Domain.Common.Entities;

namespace SessionReservation.Infrastructure.Persistence.Configurations;

public class EquipmentConfiguration: IEntityTypeConfiguration<Equipment>
{
    public void Configure(EntityTypeBuilder<Equipment> builder)
    {
        builder.Property(e => e.Name);

        builder.HasKey(e => e.Id);
        builder
            .Property(e => e.Id)
            .HasColumnType("uuid")
            .ValueGeneratedNever();
        
        builder.OwnsOne(e => e.Schedule, scheduleBuilder =>
        {
            scheduleBuilder
                .Property(s => s.Id)
                .HasColumnName("ScheduleId")
                .ValueGeneratedNever();

            scheduleBuilder
                .Property(s => s.Calendar)
                .HasColumnName("ScheduleCalendar")
                .HasJsonConversion();
        });
    }
}