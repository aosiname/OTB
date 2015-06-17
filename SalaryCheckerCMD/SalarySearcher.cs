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
        private readonly IEmployeeRepository _employeeRepository;
        //private readonly ICurrencyRepository _currencyRepository;
        //private readonly ISalaryRepository _salaryRepository;

        public SalarySearcher(
            IEmployeeRepository employeeRepository/*,
            ICurrencyRepository currencyRepository,
            ISalaryRepository salaryRepository*/)
        {
            _employeeRepository = employeeRepository;
            /*_currencyRepository = currencyRepository;
            _salaryRepository = salaryRepository;*/
        }

        public IEnumerable<Employee> GetEmployee(string name)
        {
            return this._employeeRepository.FindByName(name);
        }

    }
}
