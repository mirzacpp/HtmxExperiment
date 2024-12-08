namespace HtmxProject.Models.Base;

public sealed class PageModel
{
    public long TotalItemsCount { get; set; }

    public int ShownItemsCount { get; }

    public int PageSize { get; set; }

    public int CurrentPage { get; set; }

    public int TotalPageCount { get; set; }

    public int ShowingFrom { get; set; }

    public int ShowingTo { get; set; }

    public int PreviousPage { get; set; }

    public int NextPage { get; set; }

    public string PageUrl { get; set; }

    public int ShownPageCount { get; set; }

    public PageModel(long totalItemsCount, int shownItemsCount, int currentPage, int pageSize, string pageUrl)
    {
        TotalItemsCount = totalItemsCount;
        ShownItemsCount = shownItemsCount;
        CurrentPage = currentPage;
        PageSize = pageSize;
        PageUrl = pageUrl;

        TotalPageCount = (int)Math.Ceiling(Convert.ToDouble(TotalItemsCount) / PageSize);

        if (currentPage > TotalPageCount)
        {
            CurrentPage = TotalPageCount;
        }
        else if (currentPage < 1)
        {
            CurrentPage = 1;
        }
        else
        {
            CurrentPage = currentPage;
        }

        ShowingFrom = totalItemsCount == 0 ? 0 : (CurrentPage - 1) * PageSize + 1;
        ShowingTo = totalItemsCount == 0 ? 0 : (int)Math.Min(ShowingFrom + PageSize - 1, totalItemsCount);
        PreviousPage = CurrentPage <= 1 ? 1 : CurrentPage - 1;
        NextPage = CurrentPage >= TotalPageCount ? CurrentPage : CurrentPage + 1;
        ShownPageCount = 5;
    }

    public (int startPage, int endPage) GetPageIndexPoints()
    {
        var pageIndexOffset = ShownPageCount / 2;
        var startIndex = CurrentPage + pageIndexOffset > ShownPageCount ? CurrentPage - pageIndexOffset : 1;
        if (TotalPageCount - CurrentPage < pageIndexOffset)
        {
            startIndex -= CurrentPage - TotalPageCount + pageIndexOffset;
        }

        return (startIndex, startIndex + 5 - 1);
    }
}
