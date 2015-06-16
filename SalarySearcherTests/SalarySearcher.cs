using System;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;


namespace SalarySearcherTests
{
    [TestFixture]
    public class SalarySearcher
    {
        Employee employee1, employee2, employee3;
        List<Employee> employees;
        Mock<IEmployeeRepository> mockEmployeeRepository;
        IEmployeeRepository EmployeeDatabaseMock;

        Salary salary1, salary2, salary3;
        List<Salary> salaries;
        Mock<ISalaryRepository> mockSalaryRepository;
        ISalaryRepository SalaryDatabaseMock;

        private Currency currency1, currency2, currency3, currency4;
        private List<Currency> currencies;
        private Mock<ICurrencyRepository> mockCurrencyRepository;
        private ICurrencyRepository CurrencyDatabaseMock;

        [SetUp]
        public void Initialise()
        {
            // employees
            employee1 = new Employee() { Id = 1, name = "Homer Simpson" };
            employee2 = new Employee() { Id = 4, name = "Fred Flintstone" };
            employee3 = new Employee() { Id = 3, name = "Eric Cartman" };

            employees = new List<Employee> {employee1, employee2, employee3};

            mockEmployeeRepository = new Mock<IEmployeeRepository>();

            // mock implement interface
            mockEmployeeRepository.Setup(emps => emps.GetEmployees())
                .Returns(employees);

            mockEmployeeRepository.Setup(emps => emps.FindByName(It.IsAny<string>()))
                .Returns((string s) => employees.Where(emp => emp.name.ToLower().Contains(s.ToLower())).ToList<Employee>());

            // mock db is ready
            EmployeeDatabaseMock = mockEmployeeRepository.Object;

            // salaries
            salary1 = new Salary() { Id = 1, employee_id = 1, currency = 2, annual_amount = 22000m };
            salary2 = new Salary() { Id = 4, employee_id = 4, currency = 3, annual_amount = 900000m };
            salary3 = new Salary() { Id = 3, employee_id = 3, currency = 4, annual_amount = 60000m };

            salaries = new List<Salary> {salary1, salary2, salary3};

            mockSalaryRepository = new Mock<ISalaryRepository>();

            mockSalaryRepository.Setup(sals => sals.GetEmployeeSalary(
                It.IsAny<int>())).Returns((int e) => salaries.First(sal => sal.employee_id == e));

            SalaryDatabaseMock = mockSalaryRepository.Object;

            // currencies
            currency1 = new Currency() { Id = 1, unit = "GBP", conversion_factor = 1.00m };
            currency2 = new Currency() { Id = 2, unit = "USD", conversion_factor = 1.54m };
            currency3 = new Currency() { Id = 3, unit = "Rocks", conversion_factor = 10.00m };
            currency4 = new Currency() { Id = 4, unit = "Sweets", conversion_factor = 12.00m };

            currencies = new List<Currency> { currency1, currency2, currency3, currency4 };

            mockCurrencyRepository = new Mock<ICurrencyRepository>();

            mockCurrencyRepository.Setup(sals => sals.GetCurrency(
                It.IsAny<int>())).Returns((int c) => currencies.First(cur => cur.Id == c));

            CurrencyDatabaseMock = mockCurrencyRepository.Object;
        }

        [Test]
        public void SearchEmployee()
        {
            // Act
            var results = EmployeeDatabaseMock.GetEmployees();

            // Assert
            int expected = 3;
            int actual = results.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestCase("Homer Simpson", 1)]
        [TestCase("Fred", 1)]
        [TestCase("fred", 1)]
        [TestCase("Homer", 1)]
        [TestCase("e", 3)] // all
        [TestCase("er", 2)] // homer, eric7
        [TestCase("zzz", 0)] // no results
        public void SearchEmployeeByName(string name, int expected)
        {
            // Act
            var results = EmployeeDatabaseMock.FindByName(name);

            // Assert
            int actual = results.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestCase(1, 22000)]
        [TestCase(4, 900000)]
       // [TestCase(999, 900000)] // no result
        public void SearchEmployeeSalary(int employeeID, decimal expected)
        {
            Salary result = SalaryDatabaseMock.GetEmployeeSalary(employeeID);
            decimal actual = result.annual_amount;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetSalaryCurrency()
        {
            Salary salary = SalaryDatabaseMock.GetEmployeeSalary(1);
            Currency c = CurrencyDatabaseMock.GetCurrency(salary.currency);

        }
    }
}
