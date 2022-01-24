using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.Common.Lists
{
    public class PagedData<T>
    {
        public List<T> Items { get; private set; }

        public int PageIndex { get; private set; }

        public int TotalPages { get; private set; }

        public bool HasPreviousPage => (PageIndex > 1);

        public bool HasNextPage => (PageIndex < TotalPages);


        public PagedData(IEnumerable<T> items, int count, int pageIndex, int pageSize)
        {
            Items = new List<T>(items);
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }


        public static async Task<PagedData<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedData<T>(items, count, pageIndex, pageSize);
        }
    }
}
