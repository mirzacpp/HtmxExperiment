namespace HtmxProject.Application.Base
{
    public class ListResultDto<T>
    {
        public IReadOnlyList<T> Items
        {
            get => _items ??= new List<T>();
            set => _items = value;
        }

        private IReadOnlyList<T>? _items;

        public ListResultDto(IReadOnlyList<T> items)
        {
            Items = items;
        }
    }
}