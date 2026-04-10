namespace SessionReservation.Contracts.Common;

public abstract class Pagination<T>
{
    public abstract List<T> Items { get; }
    public abstract int TotalPages { get; }
    public abstract int PageSize { get; set; }
    public abstract int CurrentPage { get; set; }
}