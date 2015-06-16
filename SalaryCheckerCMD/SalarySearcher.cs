using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalarySearcherTests;

namespace SalaryCheckerCMD
{
    public class SalarySearcher// : IEmployeeRepository, ISalaryRepository, ICurrencyRepository
    {
        public static object GetEmployeePayDetails(IEmployeeRepository e, ISalaryRepository s, ICurrencyRepository c)
        {
            return null;
        }

        /*
        public Currency GetCurrency(int currencyID)
        {
            throw new NotImplementedException();
        }

        public Salary GetEmployeeSalary(int employeeID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> GetEmployees()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> FindByName(string name)
        {
            throw new NotImplementedException();
        }
        /**/
    }
}
