using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.IO;
using System.Linq;
using System.Text;
using TestProjectSyberry.Database_Models;

namespace TestProjectSyberry
{
    enum Days
    {
        Sunday = 1,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday
    }
    class Program
    {
        static void Main(string[] args)
        {
            var connection = ConfigurationManager.AppSettings["Database1"];
            DataContext db = new DataContext(connection);
            
            for (var index = 1; index < 8; index++)
            {
                var currentIndex = index == 7 ? 1 : index + 1;

                var dailyReports = db.time_reports
                    .Where(w =>(System.Data.Entity.SqlServer.SqlFunctions.DatePart("weekday", w.date) == currentIndex))
                    .OrderByDescending(tr => tr.hours)
                    .Include(tr => tr.employee)
                    .Take(3)
                    .ToList();

                if (dailyReports.Count != 0)
                {

                    StringBuilder sb = new StringBuilder("| " + Enum.GetName(typeof(Days), currentIndex) + " |");
                   

                    for (var i = 0; i < dailyReports.Count; i++)
                    {
                        if(dailyReports[i].employee!=null) 
                        {
                            sb.Append(" " + dailyReports[i].employee.name + " (" + Math.Round(dailyReports[i].hours, 2).ToString() + " hours" + "),");
                        }
                    }

                    sb.Remove(sb.Length - 1, 1).Append(" |");
                    Console.WriteLine(sb);
                }
                else
                {
                    StringBuilder sb = new StringBuilder("| " + Enum.GetName(typeof(Days), currentIndex) + " | " + "Nobody worked that day |");
                    Console.WriteLine(sb);
                }
            }
        }
    }
}
