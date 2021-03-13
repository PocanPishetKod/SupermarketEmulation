using SupermarketEmulation.Domain.Models.Products;
using SupermarketEmulation.Domain.Models.Supermarkets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SupermarketEmulation.Domain.Models.Receipts
{
    /// <summary>
    /// Чек
    /// </summary>
    public class Receipt
    {
        private readonly List<ReceiptPosition> _positions;

        public IReadOnlyCollection<ReceiptPosition> Positions => _positions;

        public Supermarket Supermarket { get; private set; }

        public Receipt(Supermarket supermarket, IReadOnlyCollection<Product> products)
        {
            if (products == null)
            {
                throw new ArgumentNullException(nameof(products));
            }

            if (supermarket == null)
            {
                throw new ArgumentNullException(nameof(Supermarket));
            }

            Supermarket = supermarket;
            _positions = new List<ReceiptPosition>();

            Initialize(products);
        }

        private void Initialize(IReadOnlyCollection<Product> products)
        {
            foreach (var group in products.GroupBy(p => p.ProductSpecification.Name))
            {
                _positions.Add(new ReceiptPosition(group.First().ProductSpecification, group.Count()));
            }
        }
    }
}
