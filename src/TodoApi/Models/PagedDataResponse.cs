using System.Collections.Generic;

namespace TodoApi.Models
{
    public class PagedDataResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public long TotalCount { get; set; }
        public long Page { get; set; }
        public long PageSize { get; set; }
        public long TotalPages { get; set; }
    }
}
