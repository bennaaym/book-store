using System.Linq;


namespace Book_Store.Models.DataLayer
{
    public static class QueryExtensions
    {
        public static IQueryable<T> PageBy<T>(this IQueryable<T> items, int pageNumber, int pageSize)
        {
            return items
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize);
        }
    }
}
