using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SessionReservation.Domain.Common.ValueObjects;
using SessionReservation.Domain.TrainerAggregate;
using SessionReservation.Infrastructure.Persistence.Converters;

namespace SessionReservation.Infrastructure.Persistence.Configurations;

public class TrainerConfiguration: IEntityTypeConfiguration<Trainer>
{
    public void Configure(EntityTypeBuilder<Trainer> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.GymId);

        builder
            .Property(p => p.Id)
            .HasColumnType("uuid")
            .ValueGeneratedNever();
        
        builder
            .Property("_sessionIds")
            .HasConversion<ListOfIdsConverter>()
            .HasColumnName("SessionIds");
        
        builder.OwnsOne<Schedule>("_schedule", scheduleBuilder =>
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