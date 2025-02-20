using System;
using System.Linq;
using System.Collections.Generic;

public class Program
{
    IList<Employee> employeeList;
    IList<Salary> salaryList;

    public Program()
    {
        employeeList = new List<Employee>() {
            new Employee(){ EmployeeID = 1, EmployeeFirstName = "Rajiv", EmployeeLastName = "Desai", Age = 49},
            new Employee(){ EmployeeID = 2, EmployeeFirstName = "Karan", EmployeeLastName = "Patel", Age = 32},
            new Employee(){ EmployeeID = 3, EmployeeFirstName = "Sujit", EmployeeLastName = "Dixit", Age = 28},
            new Employee(){ EmployeeID = 4, EmployeeFirstName = "Mahendra", EmployeeLastName = "Suri", Age = 26},
            new Employee(){ EmployeeID = 5, EmployeeFirstName = "Divya", EmployeeLastName = "Das", Age = 20},
            new Employee(){ EmployeeID = 6, EmployeeFirstName = "Ridhi", EmployeeLastName = "Shah", Age = 60},
            new Employee(){ EmployeeID = 7, EmployeeFirstName = "Dimple", EmployeeLastName = "Bhatt", Age = 53}
        };

        salaryList = new List<Salary>() {
            new Salary(){ EmployeeID = 1, Amount = 1000, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 1, Amount = 500, Type = SalaryType.Performance},
            new Salary(){ EmployeeID = 1, Amount = 100, Type = SalaryType.Bonus},
            new Salary(){ EmployeeID = 2, Amount = 3000, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 2, Amount = 1000, Type = SalaryType.Bonus},
            new Salary(){ EmployeeID = 3, Amount = 1500, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 4, Amount = 2100, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 5, Amount = 2800, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 5, Amount = 600, Type = SalaryType.Performance},
            new Salary(){ EmployeeID = 5, Amount = 500, Type = SalaryType.Bonus},
            new Salary(){ EmployeeID = 6, Amount = 3000, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 6, Amount = 400, Type = SalaryType.Performance},
            new Salary(){ EmployeeID = 7, Amount = 4700, Type = SalaryType.Monthly}
        };
    }

    public static void Main()
    {
        Program program = new Program();

        program.Task1();

        program.Task2();

        program.Task3();
    }

    public void Task1()
    {
        var totalSalary = from Employee in employeeList
                          join Salary in salaryList on Employee.EmployeeID equals Salary.EmployeeID
                          group Salary by Employee into employeeGroup
                          select new
                          {
                              EmployeeName = employeeGroup.Key.EmployeeFirstName + " " + employeeGroup.Key.EmployeeLastName,
                              TotalSalary = employeeGroup.Sum(s => s.Amount)
                          };
        var sortedEmployeeSalaries = totalSalary.OrderBy(x => x.TotalSalary);

        Console.WriteLine("Total Salary of all the employees with their corresponding names in ascending order of their salary.\n");

        foreach (var emp in sortedEmployeeSalaries)
        {
            Console.WriteLine($"Employee Name: {emp.EmployeeName}, Salary: {emp.TotalSalary}");
        }
    }

    public void Task2()
    {

        var empList = from Employee in employeeList orderby Employee.Age descending select new { Employee.EmployeeFirstName, Employee.EmployeeID };

        var totalSalary = from Employee in empList
                          join Salary in salaryList on Employee.EmployeeID equals Salary.EmployeeID
                          group Salary by Employee into employeeGroup
                          select new
                          {
                              EmployeeName = employeeGroup.Key.EmployeeFirstName,
                              TotalSalary = employeeGroup.Sum(s => s.Amount)
                          };
        var emp = totalSalary.Skip(1).First();
        Console.WriteLine("\nEmployee details of 2nd oldest employee including his/her total monthly salary.");
        Console.WriteLine($"{emp.EmployeeName} : {emp.TotalSalary}");

    }

    public void Task3()
    {
        var empList = from employee in employeeList
                      where employee.Age > 30
                      join salary in salaryList on employee.EmployeeID equals salary.EmployeeID
                      group salary by salary.Type into salaryGroup
                      select new
                      {

                          SalaryType = salaryGroup.Key,
                          AverageSalary = salaryGroup.Average(s => s.Amount)
                      };

        Console.WriteLine("\nmeans of Monthly, Performance, Bonus salary of employees whose age is greater than 30");
        foreach (var emp in empList)
        {
            Console.WriteLine($"Salary Type: {emp.SalaryType}, Mean: {emp.AverageSalary}");
        }
    }
}

public enum SalaryType
{
    Monthly,
    Performance,
    Bonus
}

public class Employee
{
    public int EmployeeID { get; set; }
    public string EmployeeFirstName { get; set; }
    public string EmployeeLastName { get; set; }
    public int Age { get; set; }
}

public class Salary
{
    public int EmployeeID { get; set; }
    public int Amount { get; set; }
    public SalaryType Type { get; set; }
}