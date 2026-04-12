using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SessionReservation.Domain.Common.Entities;
using SessionReservation.Domain.TrainerAggregate;
using SessionReservation.Infrastructure.Persistence.Converters;

namespace SessionReservation.Infrastructure.Persistence.Configurations;

public class TrainerConfiguration: IEntityTypeConfiguration<Trainer>
{
    public void Configure(EntityTypeBuilder<Trainer> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.UserId);
        builder.Property(t => t.Name);

        builder
            .Property(t => t.Id)
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