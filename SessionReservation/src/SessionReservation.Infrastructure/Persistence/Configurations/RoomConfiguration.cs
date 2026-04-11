using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SessionReservation.Domain.Common;
using SessionReservation.Domain.Common.Entities;
using SessionReservation.Domain.Common.ValueObjects;
using SessionReservation.Domain.RoomAggregate;
using SessionReservation.Infrastructure.Persistence.Converters;

namespace SessionReservation.Infrastructure.Persistence.Configurations;

public class RoomConfiguration: IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(r => r.Id);

        builder
            .Property("_maxDailySessions")
            .HasColumnName("MaxDailySessions")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Property(r => r.Capacity);

        builder
            .Property(r => r.Id)
            .HasColumnType("uuid")
            .ValueGeneratedNever();

        builder
            .Property("_sessionIds")
            .HasConversion<ListOfIdsConverter>()
            .HasColumnName("SessionIds")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
        
        builder.OwnsOne<Schedule>("_schedule", scheduleBuilder =>
        {
            scheduleBuilder
                .Property(s => s.Id)
                .ValueGeneratedNever()
                .HasColumnName("ScheduleId");

            scheduleBuilder
                .Property(s => s.Calendar)
                .HasColumnName("ScheduleCalendar")
                .HasJsonConversion();
        })
        .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}