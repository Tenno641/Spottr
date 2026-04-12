using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SessionReservation.Domain.Common.Entities;
using SessionReservation.Domain.Common.ValueObjects;
using SessionReservation.Domain.SessionAggregate;

namespace SessionReservation.Infrastructure.Persistence.Configurations;

public class SessionConfiguration: IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Type);
        builder.Property(s => s.Date);
        builder.Property(s => s.TimeRange);
        builder.Property(s => s.Capacity);
        
        builder
            .Property(s => s.Id)
            .HasColumnType("uuid")
            .ValueGeneratedNever();
        
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

        builder.OwnsMany<Reservation>("_reservations", reservationBuilder =>
        {
            reservationBuilder.ToTable("Reservations");

            reservationBuilder.Property(r => r.ParticipantId);

            reservationBuilder.WithOwner().HasForeignKey("SessionId");

            reservationBuilder.HasKey(r => r.Id);
            reservationBuilder
                .Property(r => r.Id)
                .HasColumnType("uuid")
                .ValueGeneratedNever();
        });

        builder.OwnsMany(s => s.Equipments, equipmentsBuilder =>
        {
            equipmentsBuilder.ToTable("SessionEquipments");
            
            equipmentsBuilder.WithOwner().HasForeignKey("SessionId");

            equipmentsBuilder.HasKey(e => e.Id);
            equipmentsBuilder.Property(e => e.Id)
                .HasColumnType("uuid")
                .ValueGeneratedNever();

            equipmentsBuilder.Property(e => e.Name);

            equipmentsBuilder.OwnsOne(e => e.Schedule, scheduleBuilder =>
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
        });
    }
}