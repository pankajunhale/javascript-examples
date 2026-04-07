namespace TodoList.Api.Application.Common;

public sealed class PaginationQuery
{
    private const int MaxPageSize = 100;
    private const int DefaultPageSize = 10;
    private int _pageSize = 10;
    private int _pageNumber = 1;

    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = value < 1 ? 1 : value;
    }

    public int PageSize
    {
        get => _pageSize;
        set
        {
            if (value < 1)
            {
                _pageSize = DefaultPageSize;
                return;
            }

            _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
    }
}
