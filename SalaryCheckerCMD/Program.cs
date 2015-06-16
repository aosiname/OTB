using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryCheckerCMD
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Employee Name");
            object empName = Console.ReadLine();
            Console.WriteLine(empName.ToString());





            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
