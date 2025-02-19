namespace ChallengePolynomius.Utils
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalRecords { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public PagedResult(List<T> items, int totalRecords, int currentPage, int pageSize)
        {
            Items = items;
            TotalRecords = totalRecords;
            CurrentPage = currentPage;
            PageSize = pageSize;
        }
    }
}
