using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketEmulation
{
    public class Product
    {
        public string Name { get; private set; }

        public Product(string name)
        {
            Name = name;
        }
    }
}
