using SupermarketEmulation.Domain.Events;
using SupermarketEmulation.Domain.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketEmulation.Domain.Models.Supermarkets
{
    public interface IReadOnlyShelf
    {
        Guid Id { get; }

        string Name { get; }

        ProductSpecification ProductSpecification { get; }

        IReadOnlyCollection<Product> Products { get; }

        event EventHandler<ShelfTakedProductEventArgs> ProductTaked;
    }
}
