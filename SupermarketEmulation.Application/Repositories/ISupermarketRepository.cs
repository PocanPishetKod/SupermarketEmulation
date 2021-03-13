using SupermarketEmulation.Domain.Models.Supermarkets;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketEmulation.Application.Repositories
{
    public interface ISupermarketRepository
    {
        Supermarket Get();

        void Save(Supermarket supermarket);
    }
}
