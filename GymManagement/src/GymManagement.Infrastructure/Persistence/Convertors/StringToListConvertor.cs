using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GymManagement.Infrastructure.Persistence.Convertors;

public class ListOfIdsConverter: ValueConverter<List<Guid>, string>
{
    public ListOfIdsConverter() : base(
        value => string.Join(',', value),
        value => value.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(Guid.Parse).ToList())
    {
        
    }
}