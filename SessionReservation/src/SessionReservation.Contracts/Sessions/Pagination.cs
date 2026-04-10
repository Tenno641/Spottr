namespace SessionReservation.Contracts.Sessions;

public class Pagination<T>
{
    public int PageIndex { get; private set; }
    public int TotalPages { get; private set; }
    public List<T> Items { get; private set; }

    private Pagination(List<T> items, int pageIndex, int pageSize)
    {
        Items = items;
        PageIndex = pageIndex;
        TotalPages = (int) Math.Ceiling((double)items.Count / pageSize);
    }

    public static Pagination<T> Create(List<T> items, int pageIndex, int pageSize = 10)
    {
        List<T> paginatedItems = items.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        
        return new Pagination<T>(paginatedItems, pageIndex, pageSize);
    }
}