using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SessionReservation.Domain.Gyms;

namespace SessionReservation.Infrastructure.Persistence.Configurations;

public class GymConfiguration: IEntityTypeConfiguration<Gym>
{
    public void Configure(EntityTypeBuilder<Gym> builder)
    {
        builder.OwnsMany(g => g.Equipments, equipmentBuilder=>
        {
            equipmentBuilder.ToTable("GymEquipments");

            equipmentBuilder.WithOwner().HasForeignKey("GymId");
            
            equipmentBuilder.HasKey(e => e.Id);
            equipmentBuilder.
                Property(e => e.Id)
                .HasColumnType("uuid")
                .ValueGeneratedNever();
            
            equipmentBuilder.Property(e => e.Name);

            equipmentBuilder.OwnsOne(e => e.Schedule, scheduleBuilder =>
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