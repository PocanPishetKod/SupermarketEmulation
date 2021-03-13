using SupermarketEmulation.Domain.Models.Supermarkets;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketEmulation.Application.Algorithms
{
    public interface ICashboxSelectionAlgorithm
    {
        IReadOnlyCashbox PickUp(Supermarket supermarket);
    }
}
