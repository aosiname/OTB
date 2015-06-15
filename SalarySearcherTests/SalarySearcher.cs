using System;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.Linq;


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

        [SetUp]
        public void Initialise()
        {
            // employees
            employee1 = new Employee() { Id = 1, name = "Homer Simpson" };
            employee2 = new Employee() { Id = 4, name = "Fred Flintstone" };
            employee3 = new Employee() { Id = 3, name = "Eric Cartman" };

            employees = new List<Employee>();
            employees.Add(employee1);
            employees.Add(employee2);
            employees.Add(employee3);

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
            salary1 = new Salary() { Id = 4, employee_id = 4, currency = 3, annual_amount = 900000m };
            salary1 = new Salary() { Id = 3, employee_id = 3, currency = 4, annual_amount = 60000m };

            salaries = new List<Salary>();
            salaries.Add(salary1);
            salaries.Add(salary2);
            salaries.Add(salary3);

            mockSalaryRepository = new Mock<ISalaryRepository>();

            mockSalaryRepository.Setup(sals => sals.GetEmployeeSalary(
                It.IsAny<int>())).Returns((int e) => salaries.Where(
                    sal => sal.employee_id != e).First());

            SalaryDatabaseMock = mockSalaryRepository.Object;
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
        [TestCase("er", 2)] // homer, eric
        public void SearchEmployeeByName(string name, int expected)
        {
            // Act
            var results = EmployeeDatabaseMock.FindByName(name);

            // Assert
            int actual = results.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestCase(1, 22000)]
        public void SearchEmployeeSalary(int employeeID, decimal expected)
        {
            Salary result = SalaryDatabaseMock.GetEmployeeSalary(employeeID);
            decimal actual = result.annual_amount;
            Assert.AreEqual(expected, actual);
        }
    }
}
