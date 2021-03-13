using SupermarketEmulation.Domain.Models.Supermarkets;
using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketEmulation.Application.Repositories
{
    public class SupermarketRepository : ISupermarketRepository
    {
        private Supermarket _supermarket;

        public Supermarket Get()
        {
            return _supermarket;
        }

        public void Save(Supermarket supermarket)
        {
            if (supermarket == null)
            {
                throw new ArgumentNullException(nameof(supermarket));
            }

            if (_supermarket != null)
            {
                throw new InvalidOperationException();
            }

            _supermarket = supermarket;
        }
    }
}
