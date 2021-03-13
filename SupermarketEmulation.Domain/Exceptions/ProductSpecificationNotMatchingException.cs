using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketEmulation.Domain.Exceptions
{
    public class ProductSpecificationNotMatchingException : Exception
    {
        public ProductSpecificationNotMatchingException(string excpected, string actual)
            : base ($"Не правильная спецификация добавляемого продукта. Ожидалось {excpected}, пришло {actual}")
        {

        }
    }
}
