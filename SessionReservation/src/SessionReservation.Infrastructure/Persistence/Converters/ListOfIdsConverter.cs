using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SessionReservation.Infrastructure.Persistence.Converters;

public class ListOfIdsConverter: ValueConverter<List<Guid>, string>
{
    public ListOfIdsConverter() : base(
        value => string.Join(',', value),
        value => value.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(Guid.Parse).ToList())
    {
    }
}