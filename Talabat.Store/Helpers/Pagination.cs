namespace Talabat.Store.Helpers
{
    public class Pagination<T>
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int Count { get; set; }

        public IReadOnlyList<T> Data { get; set; }

        public Pagination(int pagesize,int pageindex,IReadOnlyList<T>data,int count)
        {
            PageSize = pagesize;
            PageIndex = pageindex;
            Data = data;
            Count = count;
        }
    }
}
