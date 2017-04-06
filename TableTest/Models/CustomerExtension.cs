using System.Collections.Generic;
using System.Linq;

namespace TableTest.Models
{
    public static class CustomerExtension
    {
        public static bool IsFiltered(this Customer customer, IReadOnlyList<string> columFilters)
        {
            if (columFilters.All(string.IsNullOrEmpty)) return true;
            var values = customer.GetType()
                .GetProperties()
                .Select(propertyInfo => propertyInfo.GetValue(customer, null).ToString().ToLower())
                .ToList();

            var isEqual = true;
            foreach (var columFilter in columFilters.ToList())
            {
                var index = columFilters.ToList().IndexOf(columFilter);
                if (values[index].ToLower().Contains(columFilter.ToLower()) || string.IsNullOrEmpty(columFilter))
                    continue;

                isEqual = false;
            }
            return isEqual;
        }

        public static bool IsContains(this Customer customer, string search)
        {
            return
                string.IsNullOrEmpty(search) || customer.GetType().GetProperties()
                    .Any(propertyInfo => propertyInfo.GetValue(customer, null).ToString().ToLower()
                        .Contains(search.ToLower()));
        }


    }
}