using GymManagement.Domain.GymAggregate;
using GymManagement.Infrastructure.Persistence.Convertors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagement.Infrastructure.Persistence.Configurations;

public class GymConfiguration: IEntityTypeConfiguration<Gym>
{
    public void Configure(EntityTypeBuilder<Gym> builder)
    {
        builder
            .Property("_roomIds")
            .HasColumnName("RoomIds")
            .HasColumnType("varchar")
            .HasConversion<ListOfIdsConverter>();

        builder
            .Property("_maxRooms")
            .HasColumnName("MaxRooms")
            .HasColumnType("int");
        
        builder
            .Property("_trainersIds")
            .HasColumnName("TrainersIds")
            .HasConversion<ListOfIdsConverter>();

        builder
            .Property(g => g.Id)
            .HasColumnType("uuid")
            .ValueGeneratedNever();

        builder
            .HasKey(g => g.Id);
    }
}