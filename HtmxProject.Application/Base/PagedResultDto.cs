namespace HtmxProject.Application.Base
{
    public class PagedResultDto<T> : ListResultDto<T>
    {
        public long TotalCount { get; set; }

        public PagedResultDto(long totalCount, IReadOnlyList<T> items)
        : base(items)
        {
            TotalCount = totalCount;
        }
    }
}