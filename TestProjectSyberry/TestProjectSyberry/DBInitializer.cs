using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using TestProjectSyberry.Database_Models;

namespace TestProjectSyberry
{
    public class DBInitializer : CreateDatabaseIfNotExists<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            if (!context.time_reports.Any() || !context.employees.Any())
            {
                var tr11 = new Time_report { id = 11, hours = 5.5123f, date = new DateTime(2020, 10, 19) };
                var tr12 = new Time_report { id = 12, hours = 8.5234f, date = new DateTime(2020, 10, 21) };
                var tr13 = new Time_report { id = 13, hours = 9.5234f, date = new DateTime(2020, 10, 22) };
                var tr14 = new Time_report { id = 14, hours = 8.5434f, date = new DateTime(2020, 10, 24) };
                var tr21 = new Time_report { id = 21, hours = 6.5344f, date = new DateTime(2020, 10, 18) };
                var tr22 = new Time_report { id = 22, hours = 7.5644f, date = new DateTime(2020, 10, 22) };
                var tr23 = new Time_report { id = 23, hours = 3.5644f, date = new DateTime(2020, 10, 24) };
                var tr31 = new Time_report { id = 31, hours = 4.5345f, date = new DateTime(2020, 10, 18) };
                var tr32 = new Time_report { id = 32, hours = 2.5343f, date = new DateTime(2020, 10, 22) };
                var tr33 = new Time_report { id = 33, hours = 3.5343f, date = new DateTime(2020, 10, 24) };
                var tr41 = new Time_report { id = 41, hours = 1.5345f, date = new DateTime(2020, 10, 19) };
                var tr42 = new Time_report { id = 42, hours = 2.5393f, date = new DateTime(2020, 10, 22) };
                var tr43 = new Time_report { id = 34, hours = 4.5343f, date = new DateTime(2020, 10, 19) };

                Time_report[] tr = new Time_report[] { tr11, tr12, tr13, tr14, tr21, tr22, tr23, tr31, tr32, tr33, tr41, tr42 ,tr43};
                context.time_reports.AddRange(tr);
                context.SaveChanges();


                var emp1 = new Employee { id = 1, name = "Dima" };
                var emp2 = new Employee { id = 2, name = "Jack", };
                var emp3 = new Employee { id = 3, name = "Tim", };
                var emp4 = new Employee { id = 4, name = "Vova", };

                List<Time_report> reportsOfFirstEmployee = new List<Time_report>() { tr11, tr12, tr13, tr14 };
                emp1.time_reports = reportsOfFirstEmployee;
                List<Time_report> reportsOfSecondEmployee = new List<Time_report>() { tr21, tr22, tr23};
                emp2.time_reports = reportsOfSecondEmployee;
                List<Time_report> reportsOfThirdEmployee = new List<Time_report>() { tr31, tr32, tr33 };
                emp3.time_reports = reportsOfThirdEmployee;
                List<Time_report> reportsOfFourthEmployee = new List<Time_report>() { tr41, tr42 , tr43};
                emp4.time_reports = reportsOfFourthEmployee;

                Employee[] emp = new Employee[] { emp1, emp2, emp3, emp4 };
                context.employees.AddRange(emp);
            }
        }
    }
}
