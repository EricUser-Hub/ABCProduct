using Product.Domain.Model;

namespace Product.API.Reply
{
    public class PagedList(List<ProductReply> selectedItemsReply, int page, int pageSize, int totalCount)
    {
        public List<ProductReply> Items { get; } = selectedItemsReply;
        public int Page { get; } = page;
        public int PageSize { get; } = pageSize;
        public int TotalCount { get; } = totalCount;
        public bool HasNextPage => Page * PageSize < TotalCount;
        public bool HasPreviousPage => PageSize > 1;
        
        public static PagedList Create(ICollection<ProductModel> query, int page, int pageSize)
        {
            var totalCount = query.Count;
            var items = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var selectedItems = new List<ProductReply>();
            items.ForEach( i => selectedItems.Add(new ProductReply(i)));
            return new PagedList(selectedItems, page, pageSize, totalCount);
        }
    }
}