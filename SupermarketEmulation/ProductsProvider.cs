using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketEmulation
{
    public static class ProductsProvider
    {
        public static IReadOnlyCollection<string> Provide()
        {
            var result = new List<string>();

            for (int i = 0; i < 30; i++)
            {
                result.Add($"Product_{i + 1}");
            }

            return result;
        }
    }
}
