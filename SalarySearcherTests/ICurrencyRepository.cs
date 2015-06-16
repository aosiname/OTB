using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalarySearcherTests
{
    interface ICurrencyRepository
    {
        Currency GetCurrency(int currencyID);
    }
}
