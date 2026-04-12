using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SessionReservation.Domain.Common.Entities;
using SessionReservation.Domain.ParticipantAggregate;
using SessionReservation.Infrastructure.Persistence.Converters;

namespace SessionReservation.Infrastructure.Persistence.Configurations;

public class ParticipantConfiguration: IEntityTypeConfiguration<Participant>
{
    public void Configure(EntityTypeBuilder<Participant> builder)
    {
        builder.HasKey(p => p.Id);

        builder
            .Property(p => p.Id)
            .HasColumnType("uuid")
            .ValueGeneratedNever();

        builder.Property(p => p.UserId);

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
        
        builder
            .Property("_sessionIds")
            .HasConversion<ListOfIdsConverter>()
            .HasColumnName("SessionIds")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}