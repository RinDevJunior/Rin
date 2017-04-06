using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace TableTest.Models
{
    public class DataService
    {
        private static IEnumerable<Customer> GetCustomers()
        {
            List<Customer> dtsource;
            using (var dc = new dataSetEntities())
            {
                dtsource = dc.Customers.ToList();
            }
            return dtsource;
        }

        public List<Customer> GetCustomersPaging(IQueryable<Customer> customers, string sortOrder, int start, int length)
        {
            return customers
                .SortBy(sortOrder).Skip(start).Take(length).ToList();
        }

        public IQueryable<Customer> GetFilterResult(string search, IReadOnlyList<string> columnFilters)
        {
            var results = GetCustomers().AsQueryable();

            results = results.Where(p => p.IsContains(search))
                .Where(p => p.IsFiltered(columnFilters));

            return results;
        }
    }
}