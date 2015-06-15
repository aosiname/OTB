using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalarySearcherTests
{
    interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployees();

        IEnumerable<Employee> FindByName(string name);
    }
}
